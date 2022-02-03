using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Dal.Repos.VehicleDevice
{
    public interface IDeviceRepository
    {
        Task<DeviceResponseModel> GetDeviceById(Guid deviceId);

        Task<int> AddDevice(DeviceInputModel model);

        Task<bool> IsDeviceExist(DeviceInputModel model);

        Task<IEnumerable<DeviceResponseModel>> GetAllDevices();
    }
}
