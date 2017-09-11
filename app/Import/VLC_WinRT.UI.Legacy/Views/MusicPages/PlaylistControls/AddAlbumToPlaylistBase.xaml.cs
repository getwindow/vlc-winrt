﻿using System.Threading.Tasks;
using Windows.UI.Xaml;
using VLC.ViewModels;

namespace VLC_WinRT.Views.MusicPages.PlaylistControls
{
    public sealed partial class AddAlbumToPlaylistBase
    {
        public AddAlbumToPlaylistBase()
        {
            this.InitializeComponent();
            this.Loaded += AddAlbumToPlaylistBase_Loaded;
        }

        private async void AddAlbumToPlaylistBase_Loaded(object sender, RoutedEventArgs e)
        {
            await Locator.MusicLibraryVM.OnNavigatedTo();
        }

        private async void NewPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            await Locator.MediaLibrary.AddNewPlaylist(playlistName.Text);
        }


        private void AddToPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            if (Locator.MediaLibrary.AddAlbumToPlaylist(null))
                Locator.NavigationService.GoBack_Specific();
        }
    }
}