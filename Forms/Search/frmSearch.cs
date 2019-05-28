using Newtonsoft.Json.Linq;
using SubtitleDownloader.Data.Model;
using SubtitleDownloader.Data.RESTClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SubtitleDownloader.Forms.Search
{
    public partial class frmSearch : Form
    {
        DataTable searchResults;
        RestClient restClient;
        List<JSONResponse> responseList;
        public bool fromMain { get; set; } // ako titl nije pronađen po hashu podiže se search forma sa fromMain = true;
        public string fromMainInitialDirectory { get; set; } // ako se tražilica poziva sa vanjske forme, koji je inicijalni direktorij

        public frmSearch(RestClient restClient)
        {
            InitializeComponent();

            this.searchResults = new DataTable();
            this.restClient = restClient;

            List<Language> languageList = new List<Language> { new Language("English", "eng"), new Language("Croatian", "hrv") };

            cmbLanguage.DataSource = languageList;
            cmbLanguage.DisplayMember = "Name";
            cmbLanguage.ValueMember = "CountryCode";

            cmbLanguage.SelectedIndex = cmbLanguage.FindString(Properties.Settings.Default.DefaultLanguage);
        }

        private void Search_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(etTitle.Text))
                btnSearch.PerformClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(etTitle.Text))
            {
                MessageBox.Show("Morate unijeti naziv.", "Info");
                return;
            }

            string json = this.restClient.SearchSubtitles(cmbLanguage.SelectedValue.ToString(), etTitle.Text, etSeason.Text, etEpisode.Text);
            responseList = new List<JSONResponse>();

            JArray root = JArray.Parse(json);
            JSONResponse responseModel = null;

            foreach (var item in root)
            {
                responseModel = new JSONResponse()
                {
                    MovieName = item["MovieName"].ToString(),
                    MovieByteSize = (long)item["MovieByteSize"],
                    SubDownloadLink = item["SubDownloadLink"].ToString(),
                    SubFileName = item["SubFileName"].ToString(),
                };

                responseList.Add(responseModel);
            }

            dataGridView1.DataSource = responseList;
            btnDownload.Enabled = responseList.Count > 0 && dataGridView1.SelectedRows.Count > 0 ? true : false;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.Visible = false;
            }

            dataGridView1.Columns["SubFileName"].Visible = true;
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = this.fromMain ? this.fromMainInitialDirectory : responseList.FirstOrDefault().ParentDirectoryPath;
                sfd.Title = dataGridView1.CurrentRow.Cells["SubFileName"].Value.ToString();
                sfd.FileName = this.fromMain ? Path.GetFileNameWithoutExtension(this.Text) + ".srt" : sfd.Title;
                sfd.DefaultExt = "srt";
                sfd.Filter = "Subtitle files (*.srt)|*.srt|All files (*.*)|*.*";
                sfd.FilterIndex = 1;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    JSONResponse response = responseList.Where(x => x.SubDownloadLink == dataGridView1.CurrentRow.Cells["SubDownloadLink"].Value.ToString()).FirstOrDefault();
                    response.FileName = System.IO.Path.GetFileName(sfd.FileName);
                    response.ParentDirectoryPath = System.IO.Path.GetDirectoryName(sfd.FileName);
                    restClient.DownloadFile(response);
                    Close();
                }
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnDownload.Enabled = dataGridView1.CurrentCell.RowIndex >= 0 ? true : false;
        }
    }
}
