using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Bal.Services.DeviceService
{
    public interface IDeviceService
    {
        Task<int> AddDevice(DeviceInputModel model);

        Task<DeviceResponseModel> GetDevice(Guid deviceId);

        Task<IEnumerable<DeviceResponseModel>> GetAllDevices();
    }
}
