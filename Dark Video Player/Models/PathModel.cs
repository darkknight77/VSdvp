using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dark_Video_Player.Models
{
    public class PathModel
    {
        public PathModel(string fPath)
        {
            FPath = fPath;
        }

        public static string FPath { get; set; }
    }
}
