using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Dal.Repos.Locations
{
    public interface IVehicleLocationMapperRepository
    {
        Task<int> AddLocation(LocationInputModel model, Guid vehicleDeviceId);

        Task<LocationResponseModel> GetLocation(LocationSearchInputModel model);

        Task<IEnumerable<LocationResponseModel>> GetLocations(Guid vehicleDeviceId);
        Task<IEnumerable<LocationResponseModel>> GetLocations(LocationSearchInputModel model);

        IQueryable<VehicleLocationMapper> GetAllVehicleLocations(LocationSearchInputModel model);
        Task<IEnumerable<LocationResponseModel>> GetLocationsByVehicleAndDeviceId(Guid vehicleId, Guid deviceId);

    }
}
