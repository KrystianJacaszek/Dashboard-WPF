using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace DashboardLib.Services
{
    public interface IGeolocationService
    {
        Task<BasicGeoposition> GetCurrentCoordinates();
        Task RequestGeolocationPermission();
    }

    public class GeolocationService : IGeolocationService
    {
        public static GeolocationService Instance
        {
            get
            {
                if (instance == null)
                    instance = new GeolocationService();

                return instance;
            }
        }

        private static GeolocationService instance;
        private Geolocator geolocator = new Geolocator();

        public async Task<BasicGeoposition> GetCurrentCoordinates()
        {
            Geoposition position = await geolocator.GetGeopositionAsync();

            return position.Coordinate.Point.Position;
        }

        public async Task RequestGeolocationPermission()
        {
            await Geolocator.RequestAccessAsync();
        }
    }
}
