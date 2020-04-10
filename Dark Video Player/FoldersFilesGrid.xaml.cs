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
        public ObservableCollection<FolderVideoModel> videoFiles = new ObservableCollection<FolderVideoModel>();

        //    IReadOnlyList<IStorageItem> files;

        PathModel pathmodel;
        public static FoldersFilesGrid FFGrid;
        
        public FoldersFilesGrid()
        {
            
            this.InitializeComponent();
            FFGrid = this;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var folderPath = (string)e.Parameter;
            var files = await FolderFileHelper.GetAllFilesFromPath(folderPath);
           
            pathmodel = new PathModel(folderPath);
            this.DataContext = pathmodel;
            if (files.Count > 0)
            {
                Debug.WriteLine("Bro im in");
                populateGrid(files);
            }
        }

        public async void populateGrid(IReadOnlyList<IStorageItem> files)
        {
            videoFiles.Clear();
            if (files.Count > 0)
            {

                foreach (var item in files)
                {

                    if (item is StorageFolder)
                    {
                        var folder = (StorageFolder)item;
                        Debug.WriteLine($"{folder.DisplayName} is a folder");
                        var count = await VideoHelper.GetVideoCountFromFolder(folder);
                        var bitmap = new BitmapImage(new Uri("ms-appx:///Assets/folder-icon.png"));
                       
                        var model = new FolderVideoModel(folder.DisplayName, count.ToString(), folder.Path, bitmap, null);
                        videoFiles.Add(model);
                    }

                    else if (item is StorageFile)
                    {
                        var file = (StorageFile)item;

                        if (VideoHelper.isVideo(file))
                        {

                            var duration = await VideoHelper.GetVideoDuration(file);
                            var bitmap = await ThumbnailHelper.getThumbnailForVideo(file);
                            var model = new FolderVideoModel(file.DisplayName, duration, file.Path, bitmap, null);
                            videoFiles.Add(model);

                        }
                    }

                }

            }

        }

        private async void _grid_ItemClick(object sender, ItemClickEventArgs e)
        {
            videoFiles.Clear();
            var itemClicked = e.ClickedItem as FolderVideoModel;
            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(itemClicked.videoPath);
            pathmodel = new PathModel(folder.Path);
            this.DataContext = pathmodel;
            var fileList = await FolderFileHelper.GetAllFilesFromFolder(folder);
            if (fileList.Count > 0)
            {
                foreach (var item in fileList)
                {
                    Debug.WriteLine(item.Name);


                    if (item is StorageFolder)
                    {
                        var fol = (StorageFolder)item;
                        Debug.WriteLine($"{folder.DisplayName} is a folder");
                        var count = await VideoHelper.GetVideoCountFromFolder(fol);
                        var bitmap = new BitmapImage(new Uri("ms-appx:///Assets/folder-icon.png"));
                       
                        var model = new FolderVideoModel(folder.DisplayName, count.ToString(), folder.Path, bitmap, null);
                        videoFiles.Add(model);
                    }

                    else if (item is StorageFile)
                    {
                        var file = (StorageFile)item;

                        if (VideoHelper.isVideo(file))
                        {

                            var duration = await VideoHelper.GetVideoDuration(file);
                            var bitmap = await ThumbnailHelper.getThumbnailForVideo(file);
                            var model = new FolderVideoModel(file.DisplayName, duration, file.Path, bitmap, null);
                            videoFiles.Add(model);

                        }
                    }

                }
            }
        }
    }
}
