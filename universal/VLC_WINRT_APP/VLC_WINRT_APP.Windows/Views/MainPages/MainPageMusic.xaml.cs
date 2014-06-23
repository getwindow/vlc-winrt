﻿/**********************************************************************
 * VLC for WinRT
 **********************************************************************
 * Copyright © 2013-2014 VideoLAN and Authors
 *
 * Licensed under GPLv2+ and MPLv2
 * Refer to COPYING file of the official project for license
 **********************************************************************/

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using VLC_WINRT.Common;
using VLC_WINRT_APP.Utility.Helpers;
using VLC_WINRT.ViewModels;
using VLC_WINRT_APP.ViewModels;
using VLC_WINRT_APP.ViewModels.MainPage;

namespace VLC_WINRT_APP.Views.MainPages
{
    public sealed partial class MainPageMusic : Page
    {
        private int _currentSection;
        private bool _isLoaded;
        public MainPageMusic()
        {
            this.InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                if (_isLoaded) return;
                UIAnimationHelper.FadeOut(TracksPanel);
                for (int i = 0; i < SectionsGrid.Children.Count; i++)
                {
                    if (i != _currentSection)
                        UIAnimationHelper.FadeOut(SectionsGrid.Children[i]);
                }
                _isLoaded = true;
            };
            this.SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            DispatchHelper.Invoke(() =>
            {
                if (sizeChangedEventArgs.NewSize.Width < 1080)
                {
                    SectionsGrid.Margin = new Thickness(40, 0, 0, 0);
                }
                else
                {
                    SectionsGrid.Margin = new Thickness(50, 0, 0, 0);
                }


                if (sizeChangedEventArgs.NewSize.Width == 320)
                {
                    FullGrid.Visibility = Visibility.Collapsed;
                    SnapGrid.Visibility = Visibility.Visible;
                    SectionsHeaderListView.Visibility = Visibility.Collapsed;
                    SectionsGrid.Margin = new Thickness(0);
                }
                else
                {
                    FullGrid.Visibility = Visibility.Visible;
                    SnapGrid.Visibility = Visibility.Collapsed;
                    SectionsHeaderListView.Visibility = Visibility.Visible;
                    SectionsGrid.Margin = new Thickness(50, 0, 0, 0);
                }
            });
        }

        private void AlbumGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var album = e.ClickedItem as MusicLibraryVM.AlbumItem;
            album.PlayAlbum.Execute(e.ClickedItem);
        }
        
        private void SectionsHeaderListView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            int i = ((VLC_WINRT.Model.Panel)e.ClickedItem).Index;
            ChangedSectionsHeadersState(i);
        }

        public void ChangedSectionsHeadersState(int i)
        {
            AlbumsByArtistSemanticZoom.IsZoomedInViewActive = true;
            AlbumsByArtistSnapSemanticZoom.IsZoomedInViewActive = true;
            if (_currentSection == i) return;
            UIAnimationHelper.FadeOut(SectionsGrid.Children[_currentSection]);
            UIAnimationHelper.FadeIn(SectionsGrid.Children[i]);
            _currentSection = i;
            for (int j = 0; j < SectionsHeaderListView.Items.Count; j++)
                Locator.MusicLibraryVM.Panels[j].Opacity = 0.4;
            Locator.MusicLibraryVM.Panels[i].Opacity = 1;
        }

        private void AlbumsByArtistSemanticZoom_OnViewChangeCompleted(object sender, SemanticZoomViewChangedEventArgs e)
        {
            try
            {
                Locator.MusicLibraryVM.ExecuteSemanticZoom();
            }
            catch
            {
                
            }
        }

        private void FavoriteAlbumItemClick(object sender, ItemClickEventArgs e)
        {
            (e.ClickedItem as MusicLibraryVM.AlbumItem).PlayAlbum.Execute(e.ClickedItem);
        }

        private void OnHeaderSemanticZoomClicked(object sender, RoutedEventArgs e)
        {
            AlbumsByArtistSnapSemanticZoom.IsZoomedInViewActive = false;
            AlbumsByArtistSemanticZoom.IsZoomedInViewActive = false;
        }
    }
}
