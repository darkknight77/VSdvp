using Dark_Video_Player.Helper;
using Dark_Video_Player.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Dark_Video_Player
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FoldersFilesGrid : Page
    {
        public ObservableCollection<VideoModel> videoFiles = new ObservableCollection<VideoModel>();
        
        IReadOnlyList<IStorageItem> files;
       
        public FoldersFilesGrid()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            files = (IReadOnlyList<IStorageItem>)e.Parameter;
            if (files.Count > 0)
            {
                Debug.WriteLine("Bro im in");
                populateGrid();     
            }
        }

        public async void populateGrid() {

            if (files.Count>0) {

                foreach (var item in files) {

                    if (item is StorageFolder) {
                        var folder = (StorageFolder)item;
                        Debug.WriteLine($"{folder.DisplayName} is a folder");
                        var count = await VideoHelper.GetVideoCountFromFolder(folder);
                        var bitmap = new BitmapImage(new Uri("ms-appx:///Assets/folder-icon.png"));
                        
                        var model = new VideoModel(folder.DisplayName, count.ToString(), folder.Path, bitmap, null);
                        videoFiles.Add(model);
                    }

                    else if (item is StorageFile)
                    {
                        var file = (StorageFile)item;

                        if (VideoHelper.isVideo(file)) { 

                        var duration = await VideoHelper.GetVideoDuration(file);
                        var bitmap = await ThumbnailHelper.getThumbnailForVideo(file);
                        var model = new VideoModel(file.DisplayName, duration, file.Path, bitmap, null);
                        videoFiles.Add(model);

                        }
                    }
                    
                }
            
            }
        
        }

    }
}
