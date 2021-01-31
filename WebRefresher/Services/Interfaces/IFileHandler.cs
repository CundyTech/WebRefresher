using System.Collections.Generic;
using System.Threading.Tasks;
using WebRefresher.Models;

namespace WebRefresher.Services
{
    public interface IFileHandler
    {
        Task SaveToFile(List<WebPage> webpages);
        Task<List<WebPage>> LoadFromFileAsync();
    }
}