using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Dark_Video_Player.Models;

namespace Dark_Video_Player.Helper
{
    class FolderFileHelper
    {
       
        public async static Task<IReadOnlyList<IStorageItem>> GetAllFilesFromFolder(StorageFolder folder) {

            StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
            return await folder.GetItemsAsync();
  
        }
        public async static Task<IReadOnlyList<IStorageItem>> GetAllFilesFromPath(String path)
        {

            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(path);
            StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
            return await folder.GetItemsAsync();

        }

        public async static Task<List<StorageFolder>> GetSubfoldersFromFolder(StorageFolder folder) {

            var list = new List<StorageFolder>();
            var items = await GetAllFilesFromFolder(folder);

            foreach (var item in items) {
                if (item.IsOfType(StorageItemTypes.Folder)) list.Add((StorageFolder)item);
            }

            return list;
        }

        public async static Task<List<string>> GetSubfoldersPathsFromFolder(StorageFolder folder)
        {
            var paths = new List<string>();
            var list = await GetSubfoldersFromFolder(folder);

            foreach (var subFolder in list) {

                paths.Add(subFolder.Path);
            }

            return paths;
        }

        public static string AddFolderToFutureAccessList(IStorageItem item) {
           string token = Guid.NewGuid().ToString();
            StorageApplicationPermissions.FutureAccessList.AddOrReplace(token, item);
            FoldersFiles.tokens.Add(token);
            return token;
        }

        public async static Task<StorageFolder> GetFolderForToken(string token)
        {
            if (!StorageApplicationPermissions.FutureAccessList.ContainsItem(token)) return null;

            return await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
        }

        public async static Task<StorageFile> GetFileForToken(string token)
        {
            if (!StorageApplicationPermissions.FutureAccessList.ContainsItem(token)) return null;
            
         return await StorageApplicationPermissions.FutureAccessList.GetFileAsync(token);
        
            

        }

        public static bool IsStorageItemAccessible(IStorageItem item) {

          return  StorageApplicationPermissions.FutureAccessList.CheckAccess(item);


        }

    }
}
