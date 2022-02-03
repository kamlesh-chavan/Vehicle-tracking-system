using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Bal.Services.LocationService
{
    public interface ILocationService
    {
        Task AddLocation(LocationInputModel model);
        Task<IEnumerable<LocationResponseModel>> GetLocations(LocationSearchInputModel model);
    }
}
