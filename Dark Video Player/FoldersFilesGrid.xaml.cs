﻿using Dark_Video_Player.Helper;
using Dark_Video_Player.Models;
using Dark_Video_Player.ViewModel;
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
using Windows.UI.Core;
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
        public List<Task<FolderVideoModel>> list = new List<Task<FolderVideoModel>>();
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
               var list = await FolderFilesViewModel.populateGrid(files);
                foreach (var item in list)
                {
                    videoFiles.Add(item);
                }
            }
        }

        

        private async void _grid_ItemClick(object sender, ItemClickEventArgs e)
        {
            videoFiles.Clear();
            var itemClicked = e.ClickedItem as FolderVideoModel;
            try
            {
                StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(itemClicked.videoPath);
                pathmodel = new PathModel(folder.Path);
                this.DataContext = pathmodel;
                var fileList = await FolderFileHelper.GetAllFilesFromFolder(folder);

                Debug.WriteLine("click disabled");
                var list = await FolderFilesViewModel.populateGrid(fileList);

                foreach (var item in list)
                {
                    videoFiles.Add(item);
                }
            }
            catch (Exception ex) {
                StorageFile file = await StorageFile.GetFileFromPathAsync(itemClicked.videoPath);
                
                Frame.Navigate(typeof(Now_Playing), file);

            }
        }
            }
        }
    

