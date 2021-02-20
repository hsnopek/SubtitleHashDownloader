using SubtitleDownloader.Common.Util;
using SubtitleDownloader.Data.Client;
using SubtitleDownloader.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleDownloader.Forms.Main.Interactors
{
    class MainInteractor : IMainInteractor
    {
        readonly ISubtitleClient SubtitleClient;

        public event SubtitlesNotFoundEventHandler OnSubtitlesNotFound;
        public event OpenSearchResultsEventHandler OnSearchResultsFound;

        public MainInteractor() 
        {
            this.SubtitleClient = new SubtitleClientFactory().BuildClient();
        }

        public List<Subtitle> FindSubtitleByHash(string[] files)
        {
            List<Subtitle> subtitleList = null;

            try
            {
                foreach (string file in files)
                {
                    byte[] moviehash = MovieHasher.ComputeMovieHash(file);
                    string hexString = MovieHasher.ToHexadecimal(moviehash);

                    string languageCode = Language.GetLanguageCode(Properties.Settings.Default.DefaultLanguage);
                    SubtitleClient.DownloadSubtitleByHash(hexString, languageCode);   // Svi rezultati pretrage za odabrani fajl

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
                            subtitleList.ForEach(item =>
                            {
                                MapFileInfoToSubtitle(selectedFile, item);
                            });

                            OnSearchResultsFound(this, new SearchResultsArgs(subtitleList, this.SubtitleClient));
                        }
                    }
                    else
                    {
                        OnSubtitlesNotFound(this, new SubtitleFoundArgs(file, new FileInfo(file)));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return subtitleList;
        }

        private void DownloadFirstResultOnly(Subtitle subtitle)
        {
            SubtitleClient.DownloadFile(subtitle);
        }

        private void MapFileInfoToSubtitle(FileInfo fileInfo, Subtitle subtitle)
        {
            subtitle.FileName = fileInfo.Name + ".srt";
            subtitle.ParentDirectoryPath = fileInfo.DirectoryName;
        }
    }

    public class SubtitleFoundArgs : EventArgs
    {
        public string File { get; set; }
        public FileInfo SelectedFile { get; set; }

        public SubtitleFoundArgs(string file, FileInfo selectedFile)
        {
            this.File = file;
            this.SelectedFile = selectedFile;
        }
    }

    public class SearchResultsArgs : EventArgs
    {
        public List<Subtitle> SubtitleList { get; set; }
        public ISubtitleClient SubtitleClient { get; set; }

        public SearchResultsArgs(List<Subtitle> subtitleList, ISubtitleClient subtitleClient)
        {
            this.SubtitleList = subtitleList;
            this.SubtitleClient = subtitleClient;
        }
    }
}
