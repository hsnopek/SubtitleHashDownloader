using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleDownloader.Data.Model
{
    public class JSONResponse
    {
        public long MovieByteSize { get; set; }
        public string SubFileName { get; set; }
        public string ParentDirectoryPath { get; set; }
        public string FileName { get; set; }
        public string MovieName { get; set; }
        public string SubDownloadLink { get; set; }

    }
}
