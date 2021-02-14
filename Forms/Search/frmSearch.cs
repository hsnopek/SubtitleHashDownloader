using Newtonsoft.Json.Linq;
using SubtitleDownloader.Common.Util;
using SubtitleDownloader.Data.Client;
using SubtitleDownloader.Data.Model;
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
        readonly ISubtitleClient subtitleClient;
        public bool FromMain { get; set; } // ako titl nije pronađen po hashu podiže se search forma sa fromMain = true;
        public string FromMainInitialDirectory { get; set; } // ako se tražilica poziva sa vanjske forme, koji je inicijalni direktorij

        public frmSearch(ISubtitleClient subtitleClient)
        {
            this.subtitleClient = subtitleClient;
            InitializeComponent();

            List<Language> languageList = new List<Language> { new Language("English", "eng"), new Language("Croatian", "hrv") };

            cmbLanguage.DataSource = languageList;
            cmbLanguage.DisplayMember = "Name";
            cmbLanguage.ValueMember = "CountryCode";

            cmbLanguage.SelectedIndex = cmbLanguage.FindString(Properties.Settings.Default.DefaultLanguage);
        }

        private void Search_Load(object sender, EventArgs e)
        {
            WindowManager.SetTopMost(this.Handle);

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

            List<Subtitle> subtitleList = this.subtitleClient.SearchSubtitles(cmbLanguage.SelectedValue.ToString(), etTitle.Text, etSeason.Text, etEpisode.Text);

            dataGridView1.DataSource = subtitleList;
            btnDownload.Enabled = subtitleList != null && subtitleList.Count > 0 && dataGridView1.SelectedRows.Count > 0;

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
                Subtitle subtitle = (Subtitle) dataGridView1.CurrentRow.DataBoundItem;

                sfd.InitialDirectory = this.FromMain ? this.FromMainInitialDirectory : subtitle.ParentDirectoryPath;
                sfd.Title = dataGridView1.CurrentRow.Cells["SubFileName"].Value.ToString();
                sfd.FileName = this.FromMain ? Path.GetFileNameWithoutExtension(this.Text) + ".srt" : sfd.Title;
                sfd.DefaultExt = "srt";
                sfd.Filter = "Subtitle files (*.srt)|*.srt|All files (*.*)|*.*";
                sfd.FilterIndex = 1;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    subtitle.FileName = System.IO.Path.GetFileName(sfd.FileName);
                    subtitle.ParentDirectoryPath = Path.GetDirectoryName(sfd.FileName);
                    FileHelper.DownloadFile(subtitle);
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
