using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DashboardLib.Models
{
    //NEWS CLASSES
    public class News
    {
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
    public class Http
    {
        public static async Task<string> GetVsAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
    public class Config
    {
        public static async Task<News.Rootobject> Deserialize(string URI)
        {
            var json = await Http.GetVsAsync(URI);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<News.Rootobject>(json);
        }
    }
    //NEWS CLASSES END

    public class NewsModel
    {
        const string PL_SPORT = "https://newsapi.org/v2/top-headlines?country=pl&category=sports&apiKey=3d9608e2e5044857984732bfb6c0e0b2";
        const string USA_ENTERTAIN_NEWS = "https://newsapi.org/v2/top-headlines?country=us&category=entertainment&apiKey=3d9608e2e5044857984732bfb6c0e0b2";
        const string USA_SPORT_NEWS = "https://newsapi.org/v2/top-headlines?country=us&category=sports&apiKey=3d9608e2e5044857984732bfb6c0e0b2";
        const string PL_TECH_NEWS = "https://newsapi.org/v2/top-headlines?sources=google-news&apiKey=3d9608e2e5044857984732bfb6c0e0b2";
        const string PL_BISS_NEWS = "https://newsapi.org/v2/top-headlines?country=pl&category=business&apiKey=3d9608e2e5044857984732bfb6c0e0b2";
        const string PL_ENTERTAIN_NEWS = "https://newsapi.org/v2/top-headlines?country=pl&category=entertainment&apiKey=3d9608e2e5044857984732bfb6c0e0b2";

        public List<string> URL_Collection;

        public News.Rootobject Root { get; set; }
        public int NewsCounter { get; set; } = 1;
        public int SourceCounter { get; set; } = 0;

        public void SetupList()
        {
            this.URL_Collection = new List<string>();
            this.URL_Collection.Add(PL_BISS_NEWS);
            this.URL_Collection.Add(PL_TECH_NEWS);
            this.URL_Collection.Add(PL_ENTERTAIN_NEWS);
            this.URL_Collection.Add(USA_ENTERTAIN_NEWS);
            this.URL_Collection.Add(USA_SPORT_NEWS);
            this.URL_Collection.Add(PL_SPORT);
        }
        public async void CallAPI()
        {
            NewsCounter = 0;
            Root = await Config.Deserialize(this.URL_Collection[SourceCounter]);
        }
    }
}

