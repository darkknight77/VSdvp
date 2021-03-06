﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark_Video_Player.Helper
{
    class FileExtensions
    {
        public static readonly string[] Document = new string[] { ".doc", ".xls", ".ppt", ".docx", ".xlsx", ".pptx", ".pdf", ".txt", ".rtf" };
        public static readonly string[] Image = new string[] { ".jpg", ".png", ".bmp", ".gif", ".tif" };
        public static readonly string[] Music = new string[] { ".mp3", ".wma", ".m4a", ".aac" };
        public static readonly string[] Video = new string[] { ".wmv", ".mp4", ".mkv", ".ts", ".avi" };
    }
}

