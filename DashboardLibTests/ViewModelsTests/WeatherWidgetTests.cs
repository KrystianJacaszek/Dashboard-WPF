
using DashboardLib.ApiModules;
using DashboardLib.Models;
using DashboardLib.Services;
using DashboardLib.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace DashboardLibTests
{
    [TestClass]
    public class WeatherWidgetTests
    {
        private WeatherWidgetViewModel VM;
        private Mock<IGeolocationService> geolocationServiceMock;
        private Mock<IWeatherApi> weatherApiMock;

        [TestInitialize]
        public async Task SetupViewModel()
        {
            BasicGeoposition mockPosition = new BasicGeoposition();
            mockPosition.Latitude = 0;
            mockPosition.Longitude = 0;
            
            WeatherForecast mockForecast = new WeatherForecast();
            #region mockForecastSetup
            ForecastEntry forecastEntry = new ForecastEntry();
            ForecastEntryDetails forecastEntryDetails = new ForecastEntryDetails();
            forecastEntryDetails.description = "test";
            forecastEntryDetails.main = "test";
            List<ForecastEntryDetails> detailsList = new List<ForecastEntryDetails>() { forecastEntryDetails };
            forecastEntry.weather = detailsList;
            forecastEntry.dt_txt = "10-10-2010 10:10:10";
            ForecastEntryMain forecastEntryMain = new ForecastEntryMain();
            forecastEntryMain.temp = 0;
            forecastEntry.main = forecastEntryMain;
            mockForecast.list = new List<ForecastEntry>() { forecastEntry };
            #endregion

            geolocationServiceMock = new Mock<IGeolocationService>();
            geolocationServiceMock.Setup(geolocationService => geolocationService.RequestGeolocationPermission()).Returns(Task.CompletedTask);
            geolocationServiceMock.Setup(geolocationService => geolocationService.GetCurrentCoordinates()).Returns(Task.FromResult(mockPosition));

            weatherApiMock = new Mock<IWeatherApi>();
            weatherApiMock.Setup(weatherApi => weatherApi.LoadWeather(0, 0)).Returns(Task.FromResult(mockForecast));

            VM = new WeatherWidgetViewModel(geolocationServiceMock.Object, weatherApiMock.Object);
            await VM.Initialize();
        }

        [TestCleanup]
        public async Task CleanupViewModel()
        {
            await VM.Destroy();
            VM = null;

            geolocationServiceMock.Reset();
            weatherApiMock.Reset();
        }

        [UITestMethod]
        public void Initialize_Optimal()
        {
            geolocationServiceMock.Verify(geolocationService => geolocationService.RequestGeolocationPermission(), Times.Once);
            geolocationServiceMock.Verify(geolocationService => geolocationService.GetCurrentCoordinates(), Times.Once);
            weatherApiMock.Verify(weatherApi => weatherApi.LoadWeather(0, 0), Times.Once);
        }

        [UITestMethod]
        public void Initialize_Successful()
        {
            Assert.IsInstanceOfType(VM.Weather, typeof(WeatherModel));
        }
    }
}
