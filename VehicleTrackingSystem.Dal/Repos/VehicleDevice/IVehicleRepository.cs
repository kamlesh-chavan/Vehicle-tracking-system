using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Dal.Repos.VehicleDevice
{
    public interface IVehicleRepository
    {
        Task<int> AddVehicle(VehicleInputModel model);

        Task<VehicleResponseModel> GetVehicleById(Guid vehicleId);

        Task<bool> IsVehicleExist(VehicleInputModel model);

        Task<IEnumerable<VehicleResponseModel>> GetAllVehicles();
    }
}
