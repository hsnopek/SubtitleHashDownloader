using SubtitleDownloader.Data.Model;
using SubtitleDownloader.Data.RESTClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubtitleDownloader.Forms.ChooseSubtitle
{
    public partial class frmChooseSubtitle : Form
    {
        RestClient client;
        List<JSONResponse> responseList;

        public frmChooseSubtitle(RestClient client, List<JSONResponse> responseList)
        {
            InitializeComponent();

            this.client = client;
            this.responseList = responseList;

            dataGridView1.DataSource = responseList;

            DataGridViewButtonColumn btnDownload = new DataGridViewButtonColumn();
            {
                btnDownload.Name = "btnDownload";
                btnDownload.HeaderText = "Download";
                btnDownload.Text = "Download";
                btnDownload.UseColumnTextForButtonValue = true;
                this.dataGridView1.Columns.Add(btnDownload);
            }

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.Visible = false;
            }

            dataGridView1.Columns["SubFileName"].Visible = true;
            dataGridView1.Columns["btnDownload"].Visible = true;

        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.OwningColumn.Name == "btnDownload")
            {
                string link = dataGridView1.CurrentRow.Cells["SubDownloadLink"].Value.ToString();

                this.client.DownloadFile(responseList.Where(o => o.SubDownloadLink == link).FirstOrDefault());

                Close();
            }
        }
    }
}
