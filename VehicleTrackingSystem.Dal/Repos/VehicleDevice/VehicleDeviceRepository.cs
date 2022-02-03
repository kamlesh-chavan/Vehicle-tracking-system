using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Context;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Dal.Repos.VehicleDevice
{
    public class VehicleDeviceRepository : BaseRepository<VehicleDeviceMapper>, IVehicleDeviceRepository
    {
        public VehicleDeviceRepository(PostgresDbContext context) : base(context)
        {

        }

        public async Task<int> AddVehicleDevice(VehicleDeviceInputModel model)
        {
            var vehicleDevice = MapVehicleDeviceInputToVehicleDevice(model);
            await AddAsync(vehicleDevice);
            return await CommitAsync();
        }

        public async Task<VehicleDeviceMapper> GetVehicleDeviceById(Guid vehicleDeviceId)
        {
            return await GetSingleWhere(x => x.VehicleDeviceMapperId == vehicleDeviceId);
        }

        public async Task<VehicleDeviceResponseModel> GetVehicleDevice(Guid vehicleDeviceId)
        {
            var data = await GetSingleWhere(x => x.VehicleDeviceMapperId == vehicleDeviceId);
            if (data == null) 
                return null;
            return MapVehicleDeviceToVehicleDeviceResponse(data);
        }

        public async Task<VehicleDeviceMapper> GetVehicleDeviceByVehicleAndDeviceId(Guid vehicleId, Guid deviceId)
        {
            return await GetSingleWhere(x => x.VehicleId == vehicleId && x.DeviceId == deviceId);
        }

        public async Task<IEnumerable<VehicleDeviceResponseModel>> GetAllVehicleDevice()
        {
            var data = All();
            if (data == null)
                return null;
            var list = new List<VehicleDeviceResponseModel>();
            foreach (var vehicle in data)
            {
                list.Add(MapVehicleDeviceToVehicleDeviceResponse(vehicle));
            }
            return list;
        }


        public async Task<bool> IsVehicleDeviceExist(VehicleDeviceInputModel model)
        {
            var data = await GetManyWhere(x => x.VehicleId == model.VehicleId);
            if (data == null)
                return false;
            return data.Any(x => x.DeviceId == model.DeviceId);
        }


        private VehicleDeviceMapper MapVehicleDeviceInputToVehicleDevice(VehicleDeviceInputModel model)
        {
            return new VehicleDeviceMapper()
            {
                VehicleId = model.VehicleId,
                DeviceId = model.DeviceId,
            };
        }

        private VehicleDeviceResponseModel MapVehicleDeviceToVehicleDeviceResponse(VehicleDeviceMapper model)
        {
            return new VehicleDeviceResponseModel()
            {
                VehicleDeviceMapperId = model.VehicleDeviceMapperId,
                DeviceId = model.DeviceId,
                VehicleId = model.VehicleId,
            };

        }
    }
}
