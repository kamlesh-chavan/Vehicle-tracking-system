using VehicleTrackingSystem.Core.Constants;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Repos.VehicleDevice;

namespace VehicleTrackingSystem.Bal.Services.VehicleService
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public async Task<int> AddVehicle(VehicleInputModel model)
        {
            if(await this.vehicleRepository.IsVehicleExist(model))
                throw new Exception(message: Messages.VehicleExists);

            return await this.vehicleRepository.AddVehicle(model);
        }

        public async Task<VehicleResponseModel> GetVehicle(Guid vehicleId)
        {
            return await this.vehicleRepository.GetVehicleById(vehicleId);
        }

        public async Task<IEnumerable<VehicleResponseModel>> GetAllVehicles()
        {
            return await this.vehicleRepository.GetAllVehicles();
        }
    }
}
