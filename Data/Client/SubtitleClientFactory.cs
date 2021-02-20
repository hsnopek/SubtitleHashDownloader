using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleDownloader.Data.Client
{

    public class SubtitleClientFactory : ISubtitleClientFactory
    {


        public ISubtitleClient BuildClient()
        {
            switch (Properties.Settings.Default.DefaultWebsite)
            {
                case "OpenSubtitles":
                    return new OpenSubtitlesClient();
                case "PodnapisiNET":
                    return new PodnapisiNETClient();
                default:
                    break;
            }
            return new OpenSubtitlesClient();
        }


    }


}
