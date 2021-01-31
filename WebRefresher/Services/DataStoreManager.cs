using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRefresher.Models;
using WebRefresher.Services.Interfaces;

namespace WebRefresher.Services
{
    public class DataStoreManager : IDataStore<WebPage>
    {
        private List<WebPage> webPages;
        private FileHandler _fileHandler;

        public DataStoreManager()
        {
            _fileHandler = new FileHandler();
        }

        public async Task<bool> AddItemAsync(WebPage item)
        {
            webPages.Add(item);
            await _fileHandler.SaveToFile(webPages);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(WebPage item)
        {
            var oldItem = webPages.FirstOrDefault(arg => arg.Id == item.Id);
            webPages.Remove(oldItem);
            webPages.Add(item);

            await _fileHandler.SaveToFile(webPages);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = webPages.FirstOrDefault(arg => arg.Id == id);
            webPages.Remove(oldItem);

            await _fileHandler.SaveToFile(webPages);

            return await Task.FromResult(true);
            }

        public async Task DeleteAllItemsAsync()
        {
           await _fileHandler.DeleteFileAsync();
        }
        public async Task<WebPage> GetItemAsync(string id)
        {
            return await Task.FromResult(webPages.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<WebPage>> GetItemsAsync(bool forceRefresh = false)
        {
            webPages = await _fileHandler.LoadFromFileAsync();

            if (webPages == null)
            {
                webPages = new List<WebPage>();
            }
            return await Task.FromResult(webPages);
        }
    }
}
