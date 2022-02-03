using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Context;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Dal.Repos.VehicleDevice
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(PostgresDbContext context) : base(context)
        {
        }

        public async Task<int> AddVehicle(VehicleInputModel model)
        {
            var location = MapVehicleInputToVehicle(model);
            await AddAsync(location);
            return await CommitAsync();
        }

        public async Task<VehicleResponseModel> GetVehicleById(Guid vehicleId)
        {
            var data = await GetSingleWhere(x => x.VehicleId == vehicleId);
            if(data == null)
                return null;
            return MapVehicleToVehicleResponse(data);
        }

        public async Task<IEnumerable<VehicleResponseModel>> GetAllVehicles()
        {
            var data = All();
            if (data == null)
                return null;
            var list = new List<VehicleResponseModel>();
            foreach (var vehicle in data)
            {
                list.Add(MapVehicleToVehicleResponse(vehicle));
            }
            return list.OrderByDescending(x=> x.CreatedDate);
        }

        public async Task<bool> IsVehicleExist(VehicleInputModel model)
        {
            var data = await GetSingleWhere(x => x.Maker.ToLower() == model.Maker.ToLower() 
                                            && x.Model == model.Model.ToLower()
                                            && x.Year == model.Year.ToLower());
            return data != null;
        }

        private Vehicle MapVehicleInputToVehicle(VehicleInputModel model)
        {
            return new Vehicle()
            {
                Maker = model.Maker,
                Model = model.Model,
                ModelNumber = model.ModelNumber,
                Year = model.Year, 
                IsActive = true,
                IsDeleted = false,
                CreatedBy = null,
                CreatedDate = DateTime.Now
            };
        }

        private VehicleResponseModel MapVehicleToVehicleResponse(Vehicle model)
        {
            return new VehicleResponseModel()
            {
                VehicleId = model.VehicleId,    
                Maker = model.Maker,
                Model = model.Model,
                ModelNumber = model.ModelNumber,
                Year = model.Year,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
            };
        }


    }
}
