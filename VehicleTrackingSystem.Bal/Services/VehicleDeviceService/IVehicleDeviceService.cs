using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Bal.Services.VehicleDeviceService
{
    public interface IVehicleDeviceService
    {
        Task<int> AddVehicleDevice(VehicleDeviceInputModel model);

        Task<VehicleDeviceResponseModel> GetVehicleDeviceById(Guid vehicleDeviceId);

        Task<IEnumerable<VehicleDeviceResponseModel>> GetAllVehicleDevice();
    }
}
