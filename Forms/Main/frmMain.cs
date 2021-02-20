using Microsoft.Win32;
using SubtitleDownloader.Common.Util;
using SubtitleDownloader.Data.Client;
using SubtitleDownloader.Data.Model;
using SubtitleDownloader.Forms.ChooseSubtitle;
using SubtitleDownloader.Forms.Main;
using SubtitleDownloader.Forms.Main.Presenters;
using SubtitleDownloader.Forms.Search;
using SubtitleDownloader.Forms.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SubtitleDownloader
{
    public partial class Main : Form, IMainView
    {
        readonly ISubtitleClientFactory SubtitleClientFactory;
        readonly IMainPresenter MainPresenter;

        public Main()
        {
            this.SubtitleClientFactory = new SubtitleClientFactory();
            this.MainPresenter = new MainPresenter(this);

            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            WindowManager.SetTopMost(this.Handle);
        }


        public void OpenSettingsForm(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.ShowDialog();
        }

        public void OpenNewSearchForm(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSearch searchForm = new frmSearch(this.SubtitleClientFactory);
            searchForm.ShowDialog();
        }

        public void ShowSearchFormWhenNoSubtitleFound(String file, FileInfo selectedFile) 
        {
            if (MessageBox.Show("Pretraga po hash-u nije pronašla niti jedan rezultat za datoteku:\n\n" + selectedFile.Name +
                            "\n\nŽelite li pretražiti po pojmu?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (frmSearch frmSearch = new frmSearch(this.SubtitleClientFactory))
                {
                    frmSearch.Text = file;
                    frmSearch.FromMain = true;
                    frmSearch.FromMainInitialDirectory = selectedFile.Directory.ToString();
                    frmSearch.etTitle.Text = Path.GetFileNameWithoutExtension(selectedFile.Name);
                    frmSearch.ShowDialog();
                }
            }
        }

        public void OpenChooseSubtitleForm(List<Subtitle> responseList, ISubtitleClient subtitleClient)
        {
            using (frmChooseSubtitle frmChooseSubtitle = new frmChooseSubtitle(responseList, subtitleClient))
            {
                frmChooseSubtitle.ShowDialog();
            }
        }


        private void ShowDragFileEffect(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        public void DropFile(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            this.MainPresenter.OnFileDrop(files);

        }
       
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && Properties.Settings.Default.MinimizeToTray)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                Application.Exit();
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (key.GetValue("SubtitleDownloader") != null)
                Hide();

        }

        public void BringToFront(object sender, EventArgs e)
        {
            Show();
        }

        public void SelectFolder(object sender, EventArgs e)
        {
            using (OpenFileDialog opf = new OpenFileDialog())
            {
                opf.Multiselect = true;
                opf.InitialDirectory = @"C:\";
                opf.Filter = "Video Files (*.avi, *.mpg, *.mpeg, *.mp4, *.m4v, *.mkv)|*.avi; *.mpg; *.mpeg; *.mp4; *.m4v; *.mkv|All Files (*.*)|*.*";

                opf.ShowDialog();

                MainPresenter.OnSelectFolderClick(opf.FileNames);
            }
        }

        public void ExitApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void StartWithWindows(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key.GetValue("SubtitleDownloader") == null)
                key.SetValue("SubtitleDownloader", Application.ExecutablePath.ToString());
            else
                key.DeleteValue("SubtitleDownloader", false);

            mnuStartWithWindows.Checked = !mnuStartWithWindows.Checked;
        }

        public void OpenContextMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

                if (key.GetValue("SubtitleDownloader") == null)
                    mnuStartWithWindows.Checked = false;
                else
                    mnuStartWithWindows.Checked = true;
            }
        }

    }
}
