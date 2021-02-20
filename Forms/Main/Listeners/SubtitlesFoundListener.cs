using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleDownloader.Forms.Main
{
    interface SubtitlesFoundListener
    {
        void OnSubtitlesFound();
        void OnSubtitlesNotFound();
    }
}
