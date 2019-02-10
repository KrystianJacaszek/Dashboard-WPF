using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace DashboardLib.ApiModules
{
    public class NewsApi
    {
        private NewsApi()
        {
            httpClient = new HttpClient();
        }

        public static NewsApi Instance
        {
            get
            {
                if (instance == null)
                    instance = new NewsApi();

                return instance;
            }
        }


        private readonly HttpClient httpClient;
        private static NewsApi instance;

        public List<string> urlList = new List<string>();

        public string UrlList()
        {
            string url = string.Empty;

            urlList.Add("https://newsapi.org/v2/top-headlines?country=pl&category=sports&apiKey=3d9608e2e5044857984732bfb6c0e0b2");
            urlList.Add("https://newsapi.org/v2/top-headlines?country=us&category=entertainment&apiKey=3d9608e2e5044857984732bfb6c0e0b2");
            urlList.Add("https://newsapi.org/v2/top-headlines?country=us&category=sports&apiKey=3d9608e2e5044857984732bfb6c0e0b2");
            urlList.Add("https://newsapi.org/v2/top-headlines?sources=google-news&apiKey=3d9608e2e5044857984732bfb6c0e0b2");
            urlList.Add("https://newsapi.org/v2/top-headlines?country=pl&category=business&apiKey=3d9608e2e5044857984732bfb6c0e0b2");
            urlList.Add("https://newsapi.org/v2/top-headlines?country=pl&category=entertainment&apiKey=3d9608e2e5044857984732bfb6c0e0b2");

            foreach (string x in urlList)
            {
                url = x;
            }

            return url;
        }


        public async Task<Rootobject> LoadNews(string url)
        {
            Rootobject rootObject = null;

            HttpResponseMessage response = await httpClient.GetAsync(UrlList());

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    rootObject = await response.Content.ReadAsAsync<Rootobject>();
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.ToString());
                }
            }
            return rootObject;
        }



        public class Rootobject
        {
            public string status { get; set; }
            public int totalResults { get; set; }
            public Article[] articles { get; set; }
        }

        public class Article
        {
            public Source source { get; set; }
            public string author { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string url { get; set; }
            public string urlToImage { get; set; }
            public DateTime publishedAt { get; set; }
            public string content { get; set; }
        }

        public class Source
        {
            public string id { get; set; }
            public string name { get; set; }
        }
    }
}
