using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DashboardLib.Models
{
    public class WeatherModel : INotifyPropertyChanged
    {
        public WeatherModel(ForecastListEntry[] forecastsList)
        {
            this.forecastsList = new ObservableCollection<ForecastListEntry>(forecastsList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
        
        private ObservableCollection<ForecastListEntry> forecastsList = new ObservableCollection<ForecastListEntry>();

        public short CurrentTemperature => forecastsList[0].Temperature;
        public string CurrentWeatherDescription => forecastsList[0].Description;
        public WeatherTypeEnum CurrentWeatherType => forecastsList[0].WeatherType;
        private ObservableCollection<ForecastListEntry> ForecastsList
        {
            get { return forecastsList; }
            set {
                if (value != forecastsList) {
                    forecastsList = value;
                    NotifyPropertyChanged("CurrentTemperature");
                    NotifyPropertyChanged("CurrentWeatherDescription");
                    NotifyPropertyChanged("CurrentWeatherType");
                    NotifyPropertyChanged("MaxTemperature");
                    NotifyPropertyChanged("MinTemperature");
                }
            }
        }
        public short MaxTemperature => forecastsList.Max(item => item.Temperature);
        public short MinTemperature => forecastsList.Min(item => item.Temperature);
    }

    public class ForecastListEntry
    {
        public string Description { get; set; }
        public short Temperature { get; set; }
        public DateTime Time { get; set; }
        public WeatherTypeEnum WeatherType { get; set; }
    }

    public enum WeatherTypeEnum
    {
        Clear = 1,
        Clouds = 2,
        Drizzle = 3,
        Extreme = 4,
        Rain = 5,
        Snow = 6,
        Thunderstorm = 7,
        Unknown = 8,
    }
}
