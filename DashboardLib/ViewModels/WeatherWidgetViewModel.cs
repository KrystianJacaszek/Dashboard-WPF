using DashboardLib.ApiModules;
using DashboardLib.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace DashboardLib.ViewModels
{
    public class WeatherWidgetViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        private bool fetchingData = false;
        private string locationCityName = "";
        private bool locationErrorThrown = false;
        private WeatherModel weather;
        private WeatherApi weatherApi = WeatherApi.Instance;

        public bool FetchingData
        {
            get { return fetchingData; }
            private set { if (value != fetchingData) { fetchingData = value; NotifyPropertyChanged("FetchingData"); } }
        }

        public string LocationCityName
        {
            get { return locationCityName; }
            private set { if (value != locationCityName) { locationCityName = value; NotifyPropertyChanged("LocationCityName"); } }
        }

        public bool LocationErrorThrown
        {
            get { return locationErrorThrown; }
            private set { if (value != locationErrorThrown) { locationErrorThrown = value; NotifyPropertyChanged("LocationErrorThrown"); } }
        }

        public WeatherModel Weather
        {
            get { return weather; }
            private set { if (value != weather) { weather = value; NotifyPropertyChanged("Weather"); } }
        }

        public async void Initialize()
        {
            FetchingData = true;

            Geoposition position = null;

            try
            {
                await Geolocator.RequestAccessAsync();
                Geolocator geolocator = new Geolocator();
                position = await geolocator.GetGeopositionAsync();
            } catch (Exception e)
            {
                LocationErrorThrown = true;
                FetchingData = false;
            }

            if (LocationErrorThrown || position == null)
                return;

            LocationCityName = await getLocationCityName(position.Coordinate.Point.Position.Latitude, position.Coordinate.Point.Position.Longitude);
            WeatherForecast weatherData = await weatherApi.LoadWeather(position.Coordinate.Point.Position.Latitude, position.Coordinate.Point.Position.Longitude);

            if (weatherData != null)
            {
                ForecastListEntry[] forecastsList = weatherData.list.Select(item => {
                    ForecastListEntry entry = new ForecastListEntry();

                    entry.Description = capitalizeFirstLetter(item.weather[0].description);
                    entry.Temperature = (short)Math.Round(convertKelvinToCelsius(item.main.temp));
                    entry.Time = Convert.ToDateTime(item.dt_txt);
                    entry.WeatherType = getWeatherTypeFor(item.weather[0].main);

                    return entry;
                }).ToArray();

                Weather = new WeatherModel(forecastsList);

                FetchingData = false;
            }
        }

        private string capitalizeFirstLetter(string word)
        {
            return char.ToUpper(word[0]) + word.Substring(1);
        }

        private float convertKelvinToCelsius(float kelvinDegrees)
        {
            return kelvinDegrees - (float)273.15;
        }

        private async Task<string> getLocationCityName(double latitude, double longitude)
        {
            BasicGeoposition location = new BasicGeoposition();
            location.Latitude = latitude;
            location.Longitude = longitude;

            Geopoint pointToReverseGeocode = new Geopoint(location);

            MapService.ServiceToken = "AgnaSQNEN_Bt6FdHg63M-H8nj55Ne3ZywzJqn0Z--WGb6czU6qxrEe6Av7nORmcU";
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);
            Debug.WriteLine("result: " + result.Status.ToString());

            if (result.Status == MapLocationFinderStatus.Success)
                 return result.Locations[0].Address.Town;

            return "Unknown City";
        }

        private WeatherTypeEnum getWeatherTypeFor(string weatherName)
        {
            switch (weatherName)
            {
                case "Clear":
                    return WeatherTypeEnum.Clear;

                case "Clouds":
                    return WeatherTypeEnum.Clouds;

                case "Drizzle":
                    return WeatherTypeEnum.Drizzle;

                case "Extreme":
                    return WeatherTypeEnum.Extreme;

                case "Rain":
                    return WeatherTypeEnum.Rain;

                case "Snow":
                    return WeatherTypeEnum.Snow;

                case "Thunderstorm":
                    return WeatherTypeEnum.Thunderstorm;

                default:
                    return WeatherTypeEnum.Unknown;
            }
        }
    }
}
