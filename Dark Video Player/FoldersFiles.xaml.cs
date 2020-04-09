using Dark_Video_Player.Helper;
using Dark_Video_Player.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
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
    public sealed partial class FoldersFiles : Page
    {
        public ObservableCollection<FolderModel> folders = new ObservableCollection<FolderModel>();
        public IReadOnlyList<StorageFile> files;

        public FoldersFiles()
        {
            this.InitializeComponent();
            //folders = FolderModel.getFolders();
        }

        async private void AddFolder_Click(object sender, RoutedEventArgs e) { 
            await getFolder();
        }



        async private Task getFolder()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;

            foreach (string extension in FileExtensions.Video) {
                folderPicker.FileTypeFilter.Add(extension);
            }

            
            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {

                FolderFileHelper.AddFolderToFutureAccessList(folder);

                var  image = new Image();
                var bitmapImage = new BitmapImage();

                files = await folder.GetFilesAsync();

                var videoList = VideoHelper.getVideosFromFolder(files,true);
                
                if (videoList.Count > 0)
                {
                    Debug.WriteLine("Trying to generate video thumbnail");
                    bitmapImage = await ThumbnailHelper.getThumbnailForVideo(videoList[0]);
                    image.Source = bitmapImage;
                    Debug.WriteLine("Thumbnail set");
                    
                }
                else {
                    bitmapImage.UriSource = new Uri("ms-appx:///Assets/folder-icon.png");
                    image.Source = bitmapImage;
                }

                var count = await VideoHelper.GetVideoCountFromFolder(folder);
                var model = new FolderModel() { title = folder.DisplayName, path = folder.Path, fileCount = count, imgSource = image.Source };

                Debug.WriteLine(model.title + model.path + model.fileCount);
                folders.Add(model);

            }
        }

        private async void GridItemClick(object sender, ItemClickEventArgs e)
        {
            var item =   e.ClickedItem as FolderModel;
            Debug.WriteLine(item.path);
            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(item.path);
            var token = FolderFileHelper.AddFolderToFutureAccessList(folder);            
            var fileList = await FolderFileHelper.GetAllFilesFromFolder(folder);


            if (fileList.Count > 0)
            {
                foreach (var file in fileList) Debug.WriteLine(file.Name);
                MainPage.rootFrame.Navigate(typeof(FoldersFilesGrid),fileList);
            }
        }
    }
}
