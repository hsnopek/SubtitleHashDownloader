using SubtitleDownloader.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubtitleDownloader.Data.RESTClient
{
    public class RestClient
    {
        readonly string baseUrl = "http://rest.opensubtitles.org/search/";

        public RestClient() { }

        public string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-User-Agent: TemporaryUserAgent");
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string responseStr = "";

            try
            {

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    responseStr = reader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {

            }
            return responseStr;
        }

        public string DownloadSubtitleByHash(string hash, string sublanguageid)
        {
            string url = baseUrl + "moviehash-" + hash + "/sublanguageid-" + sublanguageid;
            return HttpGet(url);
        }

        public string SearchSubtitles(string sublanguageid, string title, string season = "", string episode = "")
        {
            string language = "sublanguageid-" + sublanguageid;
            string query = "/query-" + title;
            string seasonStr = !string.IsNullOrEmpty(season) ? "/season-" + season : String.Empty;
            string episodeStr = !string.IsNullOrEmpty(episode) ? "/episode-" + episode : String.Empty;

            string url = baseUrl + language + query + seasonStr + episodeStr;
            return HttpGet(url);
        }

        public void DownloadFile(JSONResponse response)
        {

            string subFileName = System.IO.Path.GetFileName(response.SubDownloadLink);
            string subFullPath = Path.Combine(response.ParentDirectoryPath, subFileName);

            try
            {
                using (WebClient wc = new WebClient())
                {
                    if (!File.Exists(subFullPath))
                    {
                        wc.DownloadFile(
                                            new System.Uri(response.SubDownloadLink),
                                            subFullPath
                                        );
                    }
                }

                Decompress(new FileInfo(subFullPath), response);   // odzipaj .gz
                File.Delete(subFullPath);  // obriši .gz

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Decompress(FileInfo fileToDecompress, JSONResponse response)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);
                newFileName = Path.Combine(response.ParentDirectoryPath,response.FileName);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
                }
            }
        }
    }
}
