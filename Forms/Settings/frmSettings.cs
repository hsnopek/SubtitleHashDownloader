using SubtitleDownloader.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubtitleDownloader.Forms.Settings
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string selectedLanguage = listBoxLanguages.SelectedItem.ToString();
       
            Properties.Settings.Default.DefaultLanguage = selectedLanguage;
            Properties.Settings.Default.IFeelLucky = chkFeelLucky.Checked;
            Properties.Settings.Default.MinimizeToTray = chkMinimizeToTray.Checked;
            Properties.Settings.Default.Save();
            Close();

        }

        private void Settings_Load(object sender, EventArgs e)
        {
            WindowManager.SetTopMost(this.Handle);


            if (!string.IsNullOrEmpty(Properties.Settings.Default.DefaultLanguage))
                listBoxLanguages.SelectedItem = Properties.Settings.Default.DefaultLanguage;
            else
                listBoxLanguages.SelectedItem = "English";

            if (Properties.Settings.Default.IFeelLucky)
                chkFeelLucky.Checked = true;
            else
                chkFeelLucky.Checked = false;

            if (Properties.Settings.Default.MinimizeToTray)
                chkMinimizeToTray.Checked = true;
            else
                chkMinimizeToTray.Checked = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
