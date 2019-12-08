using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using SubtitleDownloader.Data.Model;
using SubtitleDownloader.Data.RESTClient;
using SubtitleDownloader.Forms.ChooseSubtitle;
using SubtitleDownloader.Forms.Search;
using SubtitleDownloader.Forms.Settings;
using SubtitleDownloader.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubtitleDownloader
{
    public partial class Main : Form
    {
        RestClient restClient;
        string path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        public String[] arguments;

        public Main(String[] arguments)
        {
            InitializeComponent();
            restClient = new RestClient();
            this.arguments = arguments;
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
            frmSearch searchForm = new frmSearch(restClient);
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
                    byte[] moviehash = Helpers.MovieHasher.ComputeMovieHash(file);
                    string hexString = Helpers.MovieHasher.ToHexadecimal(moviehash);

                    string languageCode = Language.GetLanguageCode(Properties.Settings.Default.DefaultLanguage);
                    string json = restClient.DownloadSubtitleByHash(hexString, languageCode);

                    FileInfo fileInfo = new FileInfo(file);
                    JArray subtitleArray = JArray.Parse(json);

                    if (subtitleArray != null && subtitleArray.Count > 0)
                    {
                        if (Properties.Settings.Default.IFeelLucky) // Dohvati samo prvi rezultat
                            DownloadFirstResult(fileInfo, subtitleArray);
                        else
                        {
                            var responseList = AddFilesToList(fileInfo, subtitleArray); // Dohvati sve rezultate i proslijedi ih u novu formu
                            using (frmChooseSubtitle frmChooseSubtitle = new frmChooseSubtitle(restClient, responseList)) frmChooseSubtitle.ShowDialog();
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Pretraga po hash-u nije pronašla niti jedan rezultat za datoteku:\n\n" + fileInfo.Name + 
                            "\n\nŽelite li pretražiti po pojmu?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            using (frmSearch frmSearch = new frmSearch(restClient))
                            {
                                frmSearch.Text = file;
                                frmSearch.fromMain = true;
                                frmSearch.fromMainInitialDirectory = fileInfo.Directory.ToString();
                                frmSearch.etTitle.Text = Path.GetFileNameWithoutExtension(fileInfo.Name);
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

        private void DownloadFirstResult(FileInfo fileInfo, JArray subtitleArray)
        {
            JSONResponse responseModel = GetResponseModel(fileInfo, subtitleArray.First);
            restClient.DownloadFile(responseModel);
        }

        private static List<JSONResponse> AddFilesToList(FileInfo fileInfo, JArray subtitleArray)
        {
            List<JSONResponse> responseList = new List<JSONResponse>();
            JSONResponse responseModel = null;

            foreach (var item in subtitleArray)
            {
                responseModel = GetResponseModel(fileInfo, item);
                responseList.Add(responseModel);
            }

            return responseList;
        }

        private static JSONResponse GetResponseModel(FileInfo fileInfo, JToken subtitleNode)
        {
            return new JSONResponse()
            {
                MovieName = subtitleNode["MovieName"].ToString(),
                MovieByteSize = (long)subtitleNode["MovieByteSize"],
                SubDownloadLink = subtitleNode["SubDownloadLink"].ToString(),
                SubFileName = subtitleNode["SubFileName"].ToString(),
                FileName = fileInfo.Name + ".srt",
                ParentDirectoryPath = fileInfo.DirectoryName
            };
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
            RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);
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
                RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);

                if (key.GetValue("SubtitleDownloader") == null)
                    mnuStartWithWindows.Checked = false;
                else
                    mnuStartWithWindows.Checked = true;
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);

            if (key.GetValue("SubtitleDownloader") != null)
                Hide();

        }
    }
}
