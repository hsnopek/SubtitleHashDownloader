using Newtonsoft.Json.Linq;
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

namespace SubtitleDownloader.Data.Client
{
    public class OpenSubtitlesClient : ISubtitleClient
    {
        public OpenSubtitlesClient() { }

        private string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-User-Agent: TemporaryUserAgent");
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string responseStr = "";

            try
            {

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            responseStr = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseStr;
        }

        public List<Subtitle> DownloadSubtitleByHash(string hash, string sublanguageid)
        {
            string url = String.Format("{0}moviehash-{1}/sublanguageid-{2}", "http://rest.opensubtitles.org/search/", hash, sublanguageid);
            String res = HttpGet(url);

            List<Subtitle> subtitleList = MapSubtitles(res);

            return subtitleList;
        }

      
        public List<Subtitle> SearchSubtitles(string sublanguageid, string title, string season = "", string episode = "")
        {
            string language = "sublanguageid-" + sublanguageid;
            string query = "/query-" + title;
            string seasonStr = !string.IsNullOrEmpty(season) ? "/season-" + season : String.Empty;
            string episodeStr = !string.IsNullOrEmpty(episode) ? "/episode-" + episode : String.Empty;

            string url = "http://rest.opensubtitles.org/search/" + language + query + seasonStr + episodeStr;
            
            String res = HttpGet(url);
            List<Subtitle> subtitleList = MapSubtitles(res);

            return subtitleList;
        }

        private List<Subtitle> MapSubtitles(string res)
        {
            List<Subtitle> subtitleList = new List<Subtitle>();
            try
            {
                JArray resArray = JArray.Parse(res);
                if (resArray != null && resArray.Count > 0)
                {
                    foreach (JToken item in resArray)
                    {
                        subtitleList.Add(new Subtitle()
                        {
                            MovieName = item["MovieName"].ToString(),
                            MovieByteSize = (long)item["MovieByteSize"],
                            SubDownloadLink = item["SubDownloadLink"].ToString(),
                            SubFileName = item["SubFileName"].ToString()
                        });
                    }
                }
            }
            catch (Exception e)
            {
            }

            return subtitleList;
        }

    }
}
