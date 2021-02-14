using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using SubtitleDownloader.Common;
using SubtitleDownloader.Common.Util;
using SubtitleDownloader.Data.Client;
using SubtitleDownloader.Data.Model;
using SubtitleDownloader.Forms.ChooseSubtitle;
using SubtitleDownloader.Forms.Search;
using SubtitleDownloader.Forms.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubtitleDownloader
{
    public partial class Main : Form
    {
        readonly ISubtitleClient subtitleClient;        

        public Main(ISubtitleClient subtitleClient)
        {
            this.subtitleClient = subtitleClient;
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            WindowManager.SetTopMost(this.Handle);
        }

        private void btnFilter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSearch searchForm = new frmSearch(subtitleClient);
            searchForm.ShowDialog();
        }

        private void Main_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            FindSubtitleByHash(files);

        }

        public void FindSubtitleByHash(string[] files)
        {
            try
            {
                foreach (string file in files)
                {
                    byte[] moviehash = MovieHasher.ComputeMovieHash(file);
                    string hexString = MovieHasher.ToHexadecimal(moviehash);

                    string languageCode = Language.GetLanguageCode(Properties.Settings.Default.DefaultLanguage);
                    List<Subtitle> subtitleList = subtitleClient.DownloadSubtitleByHash(hexString, languageCode);   // Svi rezultati pretrage za odabrani fajl

                    FileInfo selectedFile = new FileInfo(file);

                    if (subtitleList != null && subtitleList.Count > 0)
                    {
                        if (Properties.Settings.Default.IFeelLucky) // Dohvati samo prvi rezultat
                        {
                            Subtitle subtitle = subtitleList.FirstOrDefault();
                            MapFileInfoToSubtitle(selectedFile, subtitle);
                            DownloadFirstResultOnly(subtitle);
                        }
                        else // Dohvati sve rezultate i proslijedi ih u novu formu
                        {
                            subtitleList.ForEach( item => { 
                                MapFileInfoToSubtitle(selectedFile, item);
                            });

                            using (frmChooseSubtitle frmChooseSubtitle = new frmChooseSubtitle(subtitleList))
                            {
                                frmChooseSubtitle.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Pretraga po hash-u nije pronašla niti jedan rezultat za datoteku:\n\n" + selectedFile.Name + 
                            "\n\nŽelite li pretražiti po pojmu?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            using (frmSearch frmSearch = new frmSearch(subtitleClient))
                            {
                                frmSearch.Text = file;
                                frmSearch.FromMain = true;
                                frmSearch.FromMainInitialDirectory = selectedFile.Directory.ToString();
                                frmSearch.etTitle.Text = Path.GetFileNameWithoutExtension(selectedFile.Name);
                                frmSearch.ShowDialog();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DownloadFirstResultOnly(Subtitle subtitle)
        {
            FileHelper.DownloadFile(subtitle);
        }

        private void MapFileInfoToSubtitle(FileInfo fileInfo, Subtitle subtitle)
        {
            subtitle.FileName = fileInfo.Name + ".srt";
            subtitle.ParentDirectoryPath = fileInfo.DirectoryName;
        }

        private void MnuOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog opf = new OpenFileDialog())
            {
                opf.Multiselect = true;
                opf.InitialDirectory = @"C:\";
                opf.Filter = "Video Files (*.avi, *.mpg, *.mpeg, *.mp4, *.m4v, *.mkv)|*.avi; *.mpg; *.mpeg; *.mp4; *.m4v; *.mkv|All Files (*.*)|*.*";

                opf.ShowDialog();

                FindSubtitleByHash(opf.FileNames);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && Properties.Settings.Default.MinimizeToTray)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                Application.Exit();
            }
        }

        private void MnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MnuShow_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void MnuStartWithWindows_Click(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key.GetValue("SubtitleDownloader") == null)
                key.SetValue("SubtitleDownloader", Application.ExecutablePath.ToString());
            else
                key.DeleteValue("SubtitleDownloader", false);

            mnuStartWithWindows.Checked = !mnuStartWithWindows.Checked;

        }

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
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

        private void Main_Shown(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (key.GetValue("SubtitleDownloader") != null)
                Hide();

        }
    }
}
