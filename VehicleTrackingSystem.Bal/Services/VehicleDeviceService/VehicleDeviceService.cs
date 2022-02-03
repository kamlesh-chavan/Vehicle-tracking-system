using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Core.Constants;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Repos.VehicleDevice;

namespace VehicleTrackingSystem.Bal.Services.VehicleDeviceService
{
    public class VehicleDeviceService : IVehicleDeviceService
    {
        private readonly IVehicleDeviceRepository vehicleDeviceRepository;
        public VehicleDeviceService(IVehicleDeviceRepository vehicleDeviceRepository)
        {
            this.vehicleDeviceRepository = vehicleDeviceRepository;
        }

        public async Task<int> AddVehicleDevice(VehicleDeviceInputModel model)
        {
            if (await this.vehicleDeviceRepository.IsVehicleDeviceExist(model))
                throw new Exception(message: Messages.VehicleExists);

            return await this.vehicleDeviceRepository.AddVehicleDevice(model);
        }

        public async Task<VehicleDeviceResponseModel> GetVehicleDeviceById(Guid vehicleDeviceId)
        {
            return await this.vehicleDeviceRepository.GetVehicleDevice(vehicleDeviceId);
        }

        public async Task<IEnumerable<VehicleDeviceResponseModel>> GetAllVehicleDevice()
        {
            return await this.vehicleDeviceRepository.GetAllVehicleDevice();
        }
    }
}
