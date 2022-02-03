using Microsoft.Extensions.Options;
using VehicleTrackingSystem.Core.Constants;
using VehicleTrackingSystem.Core.Http;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Repos.Locations;
using VehicleTrackingSystem.Dal.Repos.VehicleDevice;

namespace VehicleTrackingSystem.Bal.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly IVehicleLocationMapperRepository vehicleLocationMapperRepository;
        private readonly IVehicleDeviceRepository vehicleDeviceRepository;
        private readonly IHttpClientAdapter httpClientAdapter;
        private readonly AppSettings constants;


        public LocationService(IVehicleLocationMapperRepository vehicleLocationMapperRepository, IVehicleDeviceRepository vehicleDeviceRepository,
            IHttpClientAdapter httpClientAdapter, IOptions<AppSettings> constants)
        {
            this.vehicleLocationMapperRepository = vehicleLocationMapperRepository;
            this.vehicleDeviceRepository = vehicleDeviceRepository;
            this.httpClientAdapter = httpClientAdapter;
            this.constants = constants.Value;

        }

        public async Task AddLocation(LocationInputModel model)
        {
            //var vehicleData = await this.vehicleRepository.GetVehicleById(model.VehicleId);
            //var deviceData = await this.deviceRepository.GetDeviceById(model.DeviceId);

            var vehicleDeviceData = await this.vehicleDeviceRepository.GetVehicleDeviceByVehicleAndDeviceId(model.VehicleId, model.DeviceId);

            if (vehicleDeviceData is null)
                throw new Exception(message: Messages.ValueDoesnMatch);

            await this.vehicleLocationMapperRepository.AddLocation(model, vehicleDeviceData.VehicleDeviceMapperId);

        }

        public async Task<IEnumerable<LocationResponseModel>> GetLocations(LocationSearchInputModel model)
        {
            var data = await this.vehicleLocationMapperRepository.GetLocations(model);


            //Get location from lat long from google geocode api. Please provide valid apiKey in appSettings.json for result.
            foreach (var item in data)
            {
                if(item?.Latitude > 0 && item?.Longitude > 0)
                {
                    string url = GenerateGoogleGeocodeURL(item.Latitude, item.Longitude);

                    var locationData = await httpClientAdapter.GetAsync<List<GeocodeApiResponseModel>>(url);
                    var geoCodeResults = locationData.Select(x=> x.results).FirstOrDefault();
                    item.Locality = geoCodeResults?.Select(x => x.formatted_address).FirstOrDefault();
                }
            }

            return data;
        }

        private string GenerateGoogleGeocodeURL(double lat, double longi)
        {
            return $"{this.constants.GoogleGeocodeReverseApi}?latlng={lat},{longi}&key={this.constants.GoogleKey}";
        }
    }
}
