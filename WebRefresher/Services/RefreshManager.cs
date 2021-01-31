using System;
using System.Collections.Generic;
using System.Text;
using WebRefresher.Models;

namespace WebRefresher.Services
{
    public class RefreshManager
    {
        private HttpManager _httpManager;
        public RefreshManager(HttpManager httpManager)
        {
            _httpManager = httpManager;
        }

        public bool RefreshWebsite(WebPage webPage)
        {
            _httpManager.CallWebPage(webPage.Url);
            return true;
        }
    }
}
