using DashboardLib.Models;
using DashboardLib.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class WeatherWidget : UserControl
    {
        public WeatherWidget()
        {
            VM = new WeatherWidgetViewModel();
            VM.Initialize();

            DataContext = VM;

            InitializeComponent();
        }

        private readonly WeatherWidgetViewModel VM;
    }

    public class CelsiusDegreesConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a string");

            return value.ToString() + "°";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }

    public class WeatherIconConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("The target must be a string");

            switch (value)
            {
                case WeatherTypeEnum.Clear:
                    return "Assets/Moon.svg";

                case WeatherTypeEnum.Clouds:
                    return "Assets/Cloud.svg";

                case WeatherTypeEnum.Drizzle:
                    return "Assets/Cubes.svg";

                case WeatherTypeEnum.Extreme:
                    return "Assets/Warning.svg";

                case WeatherTypeEnum.Rain:
                    return "Assets/Tint.svg";

                case WeatherTypeEnum.Snow:
                    return "Assets/Snowflake.svg";

                case WeatherTypeEnum.Thunderstorm:
                    return "Assets/Bolt.svg";

                case WeatherTypeEnum.Unknown:
                default:
                    return "Assets/Question.svg";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
