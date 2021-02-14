using SubtitleDownloader.Common.Util;
using SubtitleDownloader.Data.Client;
using SubtitleDownloader.Data.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SubtitleDownloader.Forms.ChooseSubtitle
{
    public partial class frmChooseSubtitle : Form
    {

        public frmChooseSubtitle(List<Subtitle> responseList)
        {
            InitializeComponent();
            WindowManager.SetTopMost(this.Handle);

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
                FileHelper.DownloadFile((Subtitle)dataGridView1.CurrentRow.DataBoundItem);
                Close();
            }
        }
    }
}
