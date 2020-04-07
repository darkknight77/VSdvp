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
using Windows.Storage.FileProperties;
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

            foreach (string extension in FileExtensions.Image) {
                folderPicker.FileTypeFilter.Add(extension);
            }

            
            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                const ThumbnailMode thumbnailMode = ThumbnailMode.PicturesView;
                const uint size = 200;
                var  image = new BitmapImage();
                using (StorageItemThumbnail thumbnail = await folder.GetThumbnailAsync(thumbnailMode, size))
                {
                    if (thumbnail != null)
                    {
                       
                        image.SetSource(thumbnail);

                    }
                }

                        var files = await folder.GetFilesAsync();

                var model = new FolderModel() { title = folder.DisplayName, path = folder.Path, fileCount = files.Count, thumbnail = image };

                Debug.WriteLine(model.title + model.path + model.fileCount);
                folders.Add(model);

            }
        }
        }
}
