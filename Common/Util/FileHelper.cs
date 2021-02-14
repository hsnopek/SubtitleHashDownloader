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

namespace SubtitleDownloader.Common.Util
{
    class FileHelper
    {
        public static void DownloadFile(Subtitle subtitle)
        {

            string subFileName = System.IO.Path.GetFileName(subtitle.SubDownloadLink);
            string subFullPath = Path.Combine(subtitle.ParentDirectoryPath, subFileName);

            try
            {
                using (WebClient wc = new WebClient())
                {
                    if (!File.Exists(subFullPath))
                    {
                        wc.DownloadFile(
                                            new System.Uri(subtitle.SubDownloadLink),
                                            subFullPath
                                        );
                    }
                }

                Decompress(new FileInfo(subFullPath), subtitle);   // odzipaj .gz
                File.Delete(subFullPath);  // obriši .gz

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void Decompress(FileInfo fileToDecompress, Subtitle response)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);
                newFileName = Path.Combine(response.ParentDirectoryPath, response.FileName);

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
