using Newtonsoft.Json.Linq;
using SubtitleDownloader.Common.Util;
using SubtitleDownloader.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleDownloader.Data.Client
{
    class PodnapisiNETClient : ISubtitleClient
    {

        private string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "application/json";
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
            return new List<Subtitle>();
        }

        public List<Subtitle> SearchSubtitles(string sublanguageid, string title, string season = "", string episode = "")
        {
            string keywords = "keywords=" + title;
            string movieType = "&movie_type=";
            string seasonStr = !string.IsNullOrEmpty(season) ? "&seasons=" + season : "&seasons=";
            string episodeStr = !string.IsNullOrEmpty(episode) ? "&episodes=" + episode : "&episodes=";
            string year = "&year=";
            string language = sublanguageid ==  "eng" ? "&language=en" : "&language=hr";

            string url = new StringBuilder()
            .Append(@"https://www.podnapisi.net/en/subtitles/search/?")
            .Append(keywords.Replace(" ", "+"))
            .Append(movieType)
            .Append(seasonStr)
            .Append(episodeStr)
            .Append(year)
            .Append(language)
           .ToString();

            String res = HttpGet(url.Replace(" ", "+"));
            List<Subtitle> subtitleList = MapResponse(res);

            return subtitleList;
        }


        public List<Subtitle> MapResponse(string res)
        {
            List<Subtitle> subtitleList = new List<Subtitle>();
            Subtitle subtitle = null;
            try
            {
                JToken resObj = JToken.Parse(res);
                JArray data = (JArray) resObj["data"];

                foreach (JToken item in data) 
                {

                    subtitle = new Subtitle();

                    subtitle.SubFileName = item.SelectToken("custom_releases").Value<JArray>().ToObject<List<String>>().FirstOrDefault<String>();
                    subtitle.SubDownloadLink = "https://www.podnapisi.net" + item.SelectToken("download").Value<String>();
                    subtitle.MovieName = item.SelectToken("movie").Value<JToken>().SelectToken("title").Value<String>();
                    subtitle.MovieByteSize = 0;

                    subtitleList.Add(subtitle);
                }
            }
            catch (Exception e)
            {
            }

            return subtitleList.Where( x => x.SubFileName != null && !String.IsNullOrEmpty(x.SubFileName)).ToList();
        }

        public void DownloadFile(Subtitle subtitle)
        {
            FileHelper.DownloadFile(subtitle, true);
        }
    }
}
