using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Dark_Video_Player.Models
{


    public class FolderModel
    {
        public string title { get; set; }
        public string path { get; set; }
        public int fileCount { get; set; }
        public BitmapImage thumbnail { get; set; }

        public FolderModel()
        { 
        }

        public FolderModel(string title, string path, int fileCount, BitmapImage image)
        {
            this.title = title;
            this.path = path;
            this.fileCount = fileCount;
            this.thumbnail = thumbnail;

        }


       /* public static List<FolderModel> getFolders() {
            List<FolderModel> folders = new List<FolderModel>();
            var model1 = new FolderModel() { title = "erh", path = "nvmbmmjh", fileCount = 5 };
            var model2 = new FolderModel() { title = "erh", path = "nvmbmmjh", fileCount = 5 };
            var model3 = new FolderModel() { title = "erh", path = "nvmbmmjh", fileCount = 5 };
            folders.Add(model1);
            folders.Add(model2);
            folders.Add(model3);
            return folders;
        }
*/

      }
}
