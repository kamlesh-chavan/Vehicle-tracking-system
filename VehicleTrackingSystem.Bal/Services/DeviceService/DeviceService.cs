using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Core.Constants;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Repos.VehicleDevice;

namespace VehicleTrackingSystem.Bal.Services.DeviceService
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository deviceRepository;
        public DeviceService(IDeviceRepository deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        public async Task<int> AddDevice(DeviceInputModel model)
        {
            if (await this.deviceRepository.IsDeviceExist(model))
                throw new Exception(message: Messages.VehicleExists);

            return await this.deviceRepository.AddDevice(model);
        }

        public async Task<DeviceResponseModel> GetDevice(Guid deviceId)
        {
            return await this.deviceRepository.GetDeviceById(deviceId);
        }

        public async Task<IEnumerable<DeviceResponseModel>> GetAllDevices()
        {
            return await this.deviceRepository.GetAllDevices();
        }
    }
}
