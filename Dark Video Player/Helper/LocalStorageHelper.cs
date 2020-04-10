using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Dark_Video_Player.Helper
{
   public class LocalStorageHelper
    {
        public static readonly string FOLDER_CONTAINER = "Folder_AccessList";

        public static void CreateContainer() {

            if (!ApplicationData.Current.LocalSettings.Containers.ContainsKey(FOLDER_CONTAINER))
            {
                ApplicationData.Current.LocalSettings.CreateContainer(FOLDER_CONTAINER, ApplicationDataCreateDisposition.Always);
            }
            else
            {
                // ApplicationData.Current.LocalSettings.Containers["Folder_AccessList"].Values.Clear();
            }
        }

        public static bool AddItemToList(string path,string token) {
            if (ApplicationData.Current.LocalSettings.Containers[FOLDER_CONTAINER].Values.ContainsKey(path) == false)
            {
                ApplicationData.Current.LocalSettings.Containers[FOLDER_CONTAINER].Values[path] = token;
                return true;
            }
            else{
                Debug.WriteLine("Duplicate entry not allowed");
            }
            return false;
        }

        public static List<string> GetAllItemsFromList()
        {
            var list = new List<string>();

            if (ApplicationData.Current.LocalSettings.Containers[FOLDER_CONTAINER].Values.Count > 0)
            {
            var iterator =  ApplicationData.Current.LocalSettings.Containers[FOLDER_CONTAINER].Values.GetEnumerator();

                while (iterator.MoveNext()) {

                    list.Add(iterator.Current.Key);

                }
   
             //   var tok = ApplicationData.Current.LocalSettings.Containers["Folder_AccessList"].Values.ElementAt(0).Value.ToString();
             //   tokens.Add(tok);
            }
            return list;
        }

    }
}
