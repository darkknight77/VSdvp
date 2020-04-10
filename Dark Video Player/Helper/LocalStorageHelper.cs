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
        static ApplicationDataContainer localSettings =  ApplicationData.Current.LocalSettings;
        public static void CreateContainer() {

            if (!localSettings.Containers.ContainsKey("Folder_AccessList"))
            {
                localSettings.CreateContainer("Folder_AccessList", ApplicationDataCreateDisposition.Always);
            }
            else
            {
                // ApplicationData.Current.LocalSettings.Containers["Folder_AccessList"].Values.Clear();
            }
        }

        public static void AddItemToList(string id,string token) {
            
            localSettings.Containers["Folder_AccessList"].Values[token] = id;
        }

        public static List<string> GetAllItemsFromList()
        {
            var list = new List<string>();
            Debug.WriteLine($" $$  {localSettings.Containers["Folder_AccessList"].Values.Count()}");
            Debug.WriteLine($" $$ {localSettings.Containers["Folder_AccessList"].Values.Count}");
            if (localSettings.Containers["Folder_AccessList"].Values.Count > 0)
            {
            var iterator = localSettings.Containers["Folder_AccessList"].Values.GetEnumerator();

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
