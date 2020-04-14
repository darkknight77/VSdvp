using Dark_Video_Player.Helper;
using Dark_Video_Player.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Dark_Video_Player
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Now_Playing : Page
    {

        public ObservableCollection<FolderVideoModel> videoFiles = new ObservableCollection<FolderVideoModel>();
        MediaPlaybackList playbackList = new MediaPlaybackList();
        public Now_Playing()
        {
            this.InitializeComponent();
            playlistView.ItemClick += PlaylistView_ItemClick;
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var file= (StorageFile)e.Parameter;
            var parent = await file.GetParentAsync();
            var list = await parent.GetFilesAsync();
            var videolist = VideoHelper.getVideosFromFolder(list, false);
            foreach (var item in videolist)
            {
                FolderVideoModel model = new FolderVideoModel()
                {
                    title = item.DisplayName,
                    duration = await VideoHelper.GetVideoDuration(item),
                    imageSource = await ThumbnailHelper.getThumbnailForVideo(item),
                    subtitle = null,
                    videoPath = item.Path,
                    mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromStorageFile(item))

                };

                videoFiles.Add(model);
                playbackList.Items.Add(model.mediaPlaybackItem);
            }
            

        }

          private async void PlaylistView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as FolderVideoModel;
            StorageFile file = await  StorageFile.GetFileFromPathAsync(item.videoPath);
            this.Frame.Navigate(typeof(MediaPlayerContainer),file);           
        }
    }
}
