using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Dal.Repos.VehicleDevice
{
    public  interface IVehicleDeviceRepository
    {
        Task<VehicleDeviceMapper> GetVehicleDeviceById(Guid vehicleDeviceId);

        Task<VehicleDeviceMapper> GetVehicleDeviceByVehicleAndDeviceId(Guid vehicleId, Guid deviceId);

        Task<int> AddVehicleDevice(VehicleDeviceInputModel model);

        Task<bool> IsVehicleDeviceExist(VehicleDeviceInputModel model);

        Task<IEnumerable<VehicleDeviceResponseModel>> GetAllVehicleDevice();

        Task<VehicleDeviceResponseModel> GetVehicleDevice(Guid vehicleDeviceId);
    }
}
