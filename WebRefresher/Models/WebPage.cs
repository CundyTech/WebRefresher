using System;

namespace WebRefresher.Models
{
    public class WebPage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int RefreshInterval { get; set; }
        public bool Enabled { get; set; }
    }
}
