using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace DashboardLib.ApiModules
{
    public class QuotesApi
    {
        private QuotesApi()
        {
            httpClient = new HttpClient();
        }

        public static QuotesApi Instance
        {
            get
            {
                if (instance == null)
                    instance = new QuotesApi();

                return instance;
            }
        }


        private readonly HttpClient httpClient;
        private static QuotesApi instance;

        private string apiUrl = "https://quotes.rest/qod";
        private Rootobject Root { get; set; }

        public async Task<Rootobject> LoadQuotes(string url)
        {
            Rootobject rootObject = null;

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

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

        public async Task<Rootobject> Deserialize(string URL)
        {
            return await LoadQuotes(URL);
        }

        public async void CallAPI()
        {
            Root = await Deserialize(apiUrl);
        }

        public class Rootobject
        {
            public Success success { get; set; }
            public Contents contents { get; set; }
        }

        public class Success
        {
            public int total { get; set; }
        }

        public class Contents
        {
            public Quote[] quotes { get; set; }
        }

        public class Quote
        {
            public string quote { get; set; }
            public string length { get; set; }
            public string author { get; set; }
            public string[] tags { get; set; }
            public string category { get; set; }
            public string date { get; set; }
            public string title { get; set; }
            public string background { get; set; }
            public string id { get; set; }
        }
    }

}
