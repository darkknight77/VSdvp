using Dark_Video_Player.Helper;
using Dark_Video_Player.Models;
using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Dark_Video_Player
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public static Frame rootFrame;
        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            rootFrame = contentFrame;
            this.NavigationCacheMode = NavigationCacheMode.Required;
            contentFrame.Navigate(typeof(FoldersFiles));
        }



        private void nav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

            NavigationViewItem item = args.SelectedItem as NavigationViewItem;

            switch (item.Tag.ToString())
            {

                case "FoldersFiles":
                    contentFrame.Navigate(typeof(FoldersFiles));
                    break;

               
            }


        }

        private async void nav_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            Debug.WriteLine("back");
            var path = PathModel.FPath;
            Debug.WriteLine(path);
           // folder = StorageFolder.GetFolderFromPathAsync(path);
            var parentDir= Directory.GetParent(path);
            Debug.WriteLine(parentDir);
            Debug.WriteLine(parentDir.FullName);
            var folder =  await  StorageFolder.GetFolderFromPathAsync(parentDir.FullName);

            if (FolderFileHelper.IsStorageItemAccessible(folder))
            {

                var pathmodel = new PathModel(parentDir.FullName);
                FoldersFilesGrid.FFGrid.DataContext = pathmodel;
                var files = await FolderFileHelper.GetAllFilesFromFolder(folder);

                // FoldersFilesGrid ff = new FoldersFilesGrid();
                FoldersFilesGrid.FFGrid.populateGrid(files);
                //ff.populateGrid(files);


           
                // if (contentFrame.CanGoBack) contentFrame.GoBack();


            }
            else {

                Debug.WriteLine($"Access Denied for {folder.DisplayName}" );

            }


        }
    }
}
