using SubtitleDownloader.Data.Client;
using SubtitleDownloader.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubtitleDownloader.Forms.Main
{
    interface IMainView
    {
        void DropFile(object sender, DragEventArgs e);
        void OpenSettingsForm(object sender, LinkLabelLinkClickedEventArgs e);
        void OpenNewSearchForm(object sender, LinkLabelLinkClickedEventArgs e);
        void BringToFront(object sender, EventArgs e);
        void SelectFolder(object sender, EventArgs e);
        void ExitApplication(object sender, EventArgs e);
        void StartWithWindows(object sender, EventArgs e);
        void OpenContextMenu(object sender, MouseEventArgs e);

        void ShowSearchFormWhenNoSubtitleFound(String file, FileInfo selectedFile);
        void OpenChooseSubtitleForm(List<Subtitle> responseList, ISubtitleClient subtitleClient);

    }
}
