using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Bal.Services.VehicleService
{
    public interface IVehicleService
    {
        Task<int> AddVehicle(VehicleInputModel model);

        Task<VehicleResponseModel> GetVehicle(Guid vehicleId);

        Task<IEnumerable<VehicleResponseModel>> GetAllVehicles();
    }
}
