using SubtitleDownloader.Data.Model;
using System.Collections.Generic;

namespace SubtitleDownloader.Data.Client
{
    public interface ISubtitleClient
    {
        List<Subtitle> DownloadSubtitleByHash(string hash, string sublanguageid);
        List<Subtitle> SearchSubtitles(string sublanguageid, string title, string season = "", string episode = "");

        List<Subtitle> MapResponse(string res);

        void DownloadFile(Subtitle subtitle);

    }
}