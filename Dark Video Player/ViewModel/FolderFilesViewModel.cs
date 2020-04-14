using Dark_Video_Player.Helper;
using Dark_Video_Player.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Dark_Video_Player.ViewModel
{
    public class FolderFilesViewModel
    {
        public static ObservableCollection<FolderVideoModel> list = new ObservableCollection<FolderVideoModel>();
        public async static Task<ObservableCollection<FolderVideoModel>> populateGrid(IReadOnlyList<IStorageItem> files)
        {
            var watch = Stopwatch.StartNew();
            list.Clear();
            
            if (files.Count > 0)
            {

                var model = new FolderVideoModel();

                foreach (var item in files)
                {

                    if (item is StorageFolder)
                    {
                        var folder = (StorageFolder)item;
                        Debug.WriteLine($"{folder.DisplayName} is a folder");
                        var count = await VideoHelper.GetVideoCountFromFolder(folder);
                        var bitmap = new BitmapImage(new Uri("ms-appx:///Assets/folder-icon.png"));
                        model = new FolderVideoModel(folder.DisplayName, count.ToString(), folder.Path, bitmap, null, null);

                    }

                    else if (item is StorageFile)
                    {
                        var file = (StorageFile)item;

                        if (VideoHelper.isVideo(file))
                        {
                            var duration = await VideoHelper.GetVideoDuration(file);
                            var bitmap = await ThumbnailHelper.getThumbnailForVideo(file);
                            model = new FolderVideoModel(file.DisplayName, duration, file.Path, bitmap, null, null);

                        }
                    }


                    list.Add(model);
                    //list.Add(Task.Run(() => model));


                }
                /* var results = await Task.WhenAll(list);

                 foreach (var item in results)
                 {
                     videoFiles.Add(item);
                 }*/

            }

            return list;
            watch.Stop();
            var time = watch.ElapsedMilliseconds;
            Debug.WriteLine($" Time -----------  {time}");
        }

    }
}
