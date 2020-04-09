using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Dark_Video_Player.Helper
{
    class ImageHelper
    {

         public static List<StorageFile> getImagesFromFolder(IReadOnlyList<StorageFile> files) {

            var list = new List<StorageFile>(); 
            
            foreach (var file in files) {

                if (isImage(file)) {
                    
                    list.Add(file);
                }

            }

            return list;

        }

       

        public static bool isImage(StorageFile file) {
         
            foreach (var ext in FileExtensions.Image)
            {
                if (Path.GetExtension(file.Path).Equals(ext)) return true; 
            }

            return false;


        }

    }
}
