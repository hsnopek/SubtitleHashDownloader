using Microsoft.Win32;
using SubtitleDownloader.Data.Model;
using SubtitleDownloader.Forms.Main.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleDownloader.Forms.Main.Presenters
{
    class MainPresenter : IMainPresenter
    {
        private readonly IMainView MainView;
        private readonly IMainInteractor MainInteractor;


        public MainPresenter(IMainView mainView)
        {
            this.MainView = mainView;
            MainInteractor = new MainInteractor();

            MainInteractor.OnSubtitlesNotFound += OnSubtitlesNotFound;
            MainInteractor.OnSearchResultsFound += OnSearchResultsFound;
        }

        public void OnBringToFrontClick()
        {
            throw new NotImplementedException();
        }

        public void OnContextMenuRightClick()
        {
            throw new NotImplementedException();
        }

        public void OnExitApplicationClick()
        {
            throw new NotImplementedException();
        }

        public void OnFileDrop(string[] files)
        {
             MainInteractor.FindSubtitleByHash(files);
        }

        public void OnOpenNewSearchForm()
        {
            throw new NotImplementedException();
        }

        public void OnOpenSettingsForm()
        {
            throw new NotImplementedException();
        }

        public void OnSelectFolderClick(string[] fileNames)
        {
            MainInteractor.FindSubtitleByHash(fileNames);
        }

        public void OnStartWithWindowsClick()
        {
            throw new NotImplementedException();
        }

        // handleri

        public void OnSearchResultsFound(object sender, SearchResultsArgs args)
        {
            this.MainView.OpenChooseSubtitleForm(args.SubtitleList, args.SubtitleClient);
        }

        public void OnSubtitlesNotFound(object sender, SubtitleFoundArgs args)
        {
            this.MainView.ShowSearchFormWhenNoSubtitleFound(args.File, args.SelectedFile);
        }


    }
}
