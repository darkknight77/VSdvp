using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Dark_Video_Player.Helper
{
   public class ThumbnailHelper
    {
        public async static Task<BitmapImage> getThumbnailForVideo(StorageFile file) {

            var bitmap = new BitmapImage();
            IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
            bitmap.SetSource(await file.GetThumbnailAsync(ThumbnailMode.SingleItem));

            return bitmap;

        }

    }
}
