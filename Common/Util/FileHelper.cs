using SubtitleDownloader.Data.Model;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace SubtitleDownloader.Common.Util
{
    class FileHelper
    {
        public static void DownloadFile(Subtitle subtitle, bool unzip)
        {
            string subFileName = System.IO.Path.GetFileName(subtitle.SubFileName);

            if (unzip && !subFileName.EndsWith(".zip"))
                subFileName += ".zip";

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

                if (unzip)
                {
                    FileHelper.Decompress(new FileInfo(subFullPath), subtitle);   // odzipaj .gz
                    File.Delete(subFullPath);  // obriši .gz
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void Decompress(FileInfo fileToDecompress, Subtitle subtitle)
        {
            string newFileName = String.Empty;

            try
            {
                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    string currentFileName = fileToDecompress.FullName;
                    newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);
                    newFileName = Path.Combine(subtitle.ParentDirectoryPath, subtitle.FileName);

                    using (FileStream decompressedFileStream = File.Create(newFileName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                        }
                    }
                }

            } catch (Exception e) // Ako dekompresija ne uspije, probaj na drugi način
            {
                using (ZipArchive archive = ZipFile.OpenRead(fileToDecompress.FullName))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        ZipFileExtensions.ExtractToFile(entry, newFileName, true);
                    }
                }
            }

        }
    }
}
