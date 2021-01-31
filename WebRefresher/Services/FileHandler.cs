using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PCLStorage;
using WebRefresher.Models;
using FileAccess = PCLStorage.FileAccess;

namespace WebRefresher.Services
{
    public class FileHandler : IFileHandler
    {
        public async Task SaveToFile(List<WebPage> webpages)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("Resources", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("WebPages.txt", CreationCollisionOption.OpenIfExists);

            await file.WriteAllTextAsync(JsonConvert.SerializeObject(webpages));
        }

        public async Task<List<WebPage>> LoadFromFileAsync()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("Resources", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("WebPages.txt", CreationCollisionOption.OpenIfExists);

            using (var reader = new StreamReader(await file.OpenAsync(FileAccess.ReadAndWrite, CancellationToken.None)))
            {
                return JsonConvert.DeserializeObject<List<WebPage>>(await reader.ReadToEndAsync());
            }
        }

        public async Task DeleteFileAsync()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("Resources", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("WebPages.txt", CreationCollisionOption.OpenIfExists);
            await file.DeleteAsync(CancellationToken.None);
        }
    }
}
