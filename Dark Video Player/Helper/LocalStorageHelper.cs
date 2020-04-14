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
        public static readonly string ACCESS_LIST_CONTAINER = "Folder_Access_List_Container";
        static ApplicationDataContainer localSettings =  ApplicationData.Current.LocalSettings;
        public static void CreateContainer() {

            if (!localSettings.Containers.ContainsKey(ACCESS_LIST_CONTAINER))
            {
                localSettings.CreateContainer(ACCESS_LIST_CONTAINER, ApplicationDataCreateDisposition.Always);
            }
            else
            {
                // ApplicationData.Current.LocalSettings.Containers[ACCESS_LIST_CONTAINER].Values.Clear();
            }
        }

        public static void AddItemToList(string id,string token) {
           
            localSettings.Containers[ACCESS_LIST_CONTAINER].Values[token] = id;
        }

        public static List<string> GetAllItemsFromList()
        {
            var list = new List<string>();
            Debug.WriteLine($" $$  {localSettings.Containers[ACCESS_LIST_CONTAINER].Values.Count()}");
            Debug.WriteLine($" $$ {localSettings.Containers[ACCESS_LIST_CONTAINER].Values.Count}");
            if (localSettings.Containers[ACCESS_LIST_CONTAINER].Values.Count > 0)
            {
           
            var iterator = localSettings.Containers[ACCESS_LIST_CONTAINER].Values.GetEnumerator();

                while (iterator.MoveNext()) {
                    
                    list.Add(iterator.Current.Key);

                }
   
             //   var tok = ApplicationData.Current.LocalSettings.Containers[ACCESS_LIST_CONTAINER].Values.ElementAt(0).Value.ToString();
             //   tokens.Add(tok);
            }
            return list;
        }

    }
}
