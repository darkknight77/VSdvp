using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls;

namespace Dark_Video_Player.Models
{
    class VideoModel
    {
        public string title { get; set; }
        public TimeSpan duration { get; set; }
        public String videoPath { get; set; }
        public Image posterImage { get; set; }
        public TimedTextSource subtitle { get; set; }


    }
}
