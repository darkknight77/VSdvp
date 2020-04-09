using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace Dark_Video_Player.Helper
{
    class VideoHelper
    {
        public static List<StorageFile> getVideosFromFolder(IReadOnlyList<StorageFile> files,bool isSingle)
        {
            

            var list = new List<StorageFile>();

            foreach (var file in files)
            {

                if (isVideo(file))
                {
                    list.Add(file);
                    if (isSingle) break;
                }

            }

            return list;

        }

        public async static Task<int> GetVideoCountFromFolder(StorageFolder folder) {

            var files = await folder.GetFilesAsync();
            var videoList = getVideosFromFolder(files, false);
            return videoList.Count;

        }

        public static bool isVideo(StorageFile file)
        {

            foreach (var ext in FileExtensions.Video)
            {
                if (Path.GetExtension(file.Path).Equals(ext)) return true;
            }

            return false;


        }

        public async static Task<string> GetVideoDuration(StorageFile file)
        {
            VideoProperties properties = await file.Properties.GetVideoPropertiesAsync();
            if (properties != null)
            {
                string[] trimTimeSpan = properties.Duration.ToString().Split(".");
                return trimTimeSpan[0];

            }

            return "0";
        }
        }
}
