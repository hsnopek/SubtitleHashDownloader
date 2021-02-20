using SubtitleDownloader.Data.Model;
using SubtitleDownloader.Forms.Main.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SubtitleDownloader.Forms.Main.Interactors.MainInteractor;

namespace SubtitleDownloader.Forms.Main
{
    public delegate void SubtitlesNotFoundEventHandler(object sender, SubtitleFoundArgs args);
    public delegate void OpenSearchResultsEventHandler(object sender, SearchResultsArgs args);

    interface IMainInteractor
    {
        event SubtitlesNotFoundEventHandler OnSubtitlesNotFound;
        event OpenSearchResultsEventHandler OnSearchResultsFound;

        List<Subtitle> FindSubtitleByHash(string[] files);
    }
}
