using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;

namespace WebRefresher.Services
{
    public class HttpManager
    {
        public string CallWebPage(string url)
        {

            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                throw new Exception(response.ErrorMessage);
            }
        }
    }
}
