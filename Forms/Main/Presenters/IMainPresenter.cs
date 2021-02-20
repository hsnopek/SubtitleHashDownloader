using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleDownloader.Forms.Main
{
    interface IMainPresenter
    {
        void OnFileDrop(string[] files);
        void OnOpenSettingsForm();
        void OnOpenNewSearchForm();
        void OnBringToFrontClick();
        void OnSelectFolderClick(string[] fileNames);
        void OnExitApplicationClick();
        void OnStartWithWindowsClick();
        void OnContextMenuRightClick();

    }
}
