using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Dark_Video_Player.Models
{
    public class VideoModel 
    {
        public VideoModel() { 
        
        }
        public VideoModel(string title, string path, int filecount, ImageSource imageSource )
        {
            this.title = title;
            this.videoPath = path;
            this.imageSource = imageSource;
            this.duration =  filecount.ToString();
        }
        public VideoModel(string title, string duration, string videoPath, ImageSource imageSource, TimedTextSource subtitle)
        {
            this.title = title;
            this.duration = duration;
            this.videoPath = videoPath;
            this.imageSource = imageSource;
            this.subtitle = subtitle;
        }

        public string title { get; set; }
        public string duration { get; set; }
        public string videoPath { get; set; }
        public ImageSource imageSource { get; set; }
        public TimedTextSource subtitle { get; set; }
        

    }
}
