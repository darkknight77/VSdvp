using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Dark_Video_Player.Models
{
    public class FolderVideoModel 
    {
        public FolderVideoModel() { 
        
        }
        public FolderVideoModel(string title, string path, int filecount, ImageSource imageSource )
        {
            this.title = title;
            this.videoPath = path;
            this.imageSource = imageSource;
            this.duration =  filecount.ToString();
        }
        public FolderVideoModel(string title, string duration, string videoPath, ImageSource imageSource, TimedTextSource subtitle, MediaPlaybackItem mediaPlaybackItem)
        {
            this.title = title;
            this.duration = duration;
            this.videoPath = videoPath;
            this.imageSource = imageSource;
            this.subtitle = subtitle;
            this.mediaPlaybackItem = mediaPlaybackItem;
        }

        public string title { get; set; }
        public string duration { get; set; }
        public string videoPath { get; set; }
        public ImageSource imageSource { get; set; }
        public MediaPlaybackItem mediaPlaybackItem { get; set; }
        public TimedTextSource subtitle { get; set; }
        

    }
}
