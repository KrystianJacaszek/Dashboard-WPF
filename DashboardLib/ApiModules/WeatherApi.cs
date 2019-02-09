using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DashboardLib.ApiModules
{
    public interface IWeatherApi
    {
        Task<WeatherForecast> LoadWeather(double latitude, double longitude);
    }

    public class WeatherApi : IWeatherApi
    {
        private WeatherApi()
        {
            httpClient = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            httpClient.BaseAddress = new Uri("http://api.openweathermap.org/");
        }

        public static WeatherApi Instance
        {
            get
            {
                if (instance == null)
                    instance = new WeatherApi();

                return instance;
            }
        }

        private static WeatherApi instance;
        private readonly string appId = "2bb1f33fae9a0b079aa08055ec6675bd";
        private readonly HttpClient httpClient;

        public async Task<WeatherForecast> LoadWeather(double latitude, double longitude)
        {
            WeatherForecast weatherForecast = null;

            HttpResponseMessage response = await httpClient.GetAsync(buildRequestPath("forecast", "lat=" + latitude + "&lon=" + longitude));

            if (response.IsSuccessStatusCode)
                try
                {
                    weatherForecast = await response.Content.ReadAsAsync<WeatherForecast>();
                } catch (Exception exception)
                {
                    Debug.WriteLine(exception.ToString());
                }

            return weatherForecast;
        }

        private string buildRequestPath(string path, string querystring)
        {
            if (querystring.Length > 0)
            {
                return "/data/2.5/" + path + "?" + querystring + "&appId=" + appId;
            } else
            {
                return "/data/2.5/" + path + "?appId=" + appId;
            }
        }
    }

    class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler): base(innerHandler) {}

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Request:");
            Debug.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Debug.WriteLine("");

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Debug.WriteLine("Response:");
            Debug.WriteLine(response.ToString());
            if (response.Content != null)
            {
                Debug.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Debug.WriteLine("");

            return response;
        }
    }

    public class Coord
    {
        public float lat { get; set; }
        public float lon { get; set; }
    }

    public class City
    {
        public Coord coord { get; set; }
        public string country { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ForecastEntryMain
    {
        public float grnd_level { get; set; }
        public float humidity { get; set; }
        public float pressure { get; set; }
        public float sea_level { get; set; }
        public float temp { get; set; }
        public float temp_kf { get; set; }
        public float temp_max { get; set; }
        public float temp_min { get; set; }
    }

    public class ForecastEntryDetails
    {
        public string description { get; set; }
        public string icon { get; set; }
        public string id { get; set; }
        public string main { get; set; }
    }

    public class WeatherEntryClouds
    {
        public float all { get; set; }
    }

    public class WeatherEntryWind
    {
        public float deg { get; set; }
        public float speed { get; set; }
    }

    public class ForecastEntry
    {
        public WeatherEntryClouds clouds { get; set; }
        public int dt { get; set; }
        public string dt_txt { get; set; }
        public ForecastEntryMain main { get; set; }
        public List<ForecastEntryDetails> weather { get; set; }
        public WeatherEntryWind wind { get; set; }
    }

    public class WeatherForecast
    {
        public City city { get; set; }
        public int cnt { get; set; }
        public string code { get; set; }
        public List<ForecastEntry> list { get; set; }
        public string message { get; set; }
    }
}
