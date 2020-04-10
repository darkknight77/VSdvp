using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Dark_Video_Player.Helper
{
   public class LocalStorageHelper
    {

        public static void CreateContainer() {

            if (!ApplicationData.Current.LocalSettings.Containers.ContainsKey("Folder_AccessList"))
            {
                ApplicationData.Current.LocalSettings.CreateContainer("Folder_AccessList", ApplicationDataCreateDisposition.Always);
            }
            else
            {
                // ApplicationData.Current.LocalSettings.Containers["Folder_AccessList"].Values.Clear();
            }
        }

        public static void AddItemToList(string token) {
            ApplicationData.Current.LocalSettings.Containers["Folder_AccessList"].Values[token] = token;
        }

        public static List<string> GetAllItemsFromList()
        {
            var list = new List<string>();

            if (ApplicationData.Current.LocalSettings.Containers["Folder_AccessList"].Values.Count > 0)
            {
            var iterator =  ApplicationData.Current.LocalSettings.Containers["Folder_AccessList"].Values.GetEnumerator();

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
