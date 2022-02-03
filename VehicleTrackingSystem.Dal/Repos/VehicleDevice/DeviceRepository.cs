using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Context;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Dal.Repos.VehicleDevice
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(PostgresDbContext context) : base(context)
        {
        }


        public async Task<int> AddDevice(DeviceInputModel model)
        {
            var location = MapDeviceInputToDevice(model);
            await AddAsync(location);
            return await CommitAsync();
        }

        public async Task<IEnumerable<DeviceResponseModel>> GetAllDevices()
        {
            var data = All();
            if (data == null)
                return null;
            var list = new List<DeviceResponseModel>();
            foreach (var vehicle in data)
            {
                list.Add(MapDeviceToDeviceResponse(vehicle));
            }
            return list.OrderByDescending(x => x.CreatedDate);
        }

        public async Task<bool> IsDeviceExist(DeviceInputModel model)
        {
            var data = await GetSingleWhere(x => x.Brand.ToLower() == model.Brand.ToLower()
                                            && x.Name == model.Name.ToLower()
                                            && x.Description == model.Description.ToLower()
                                            && x.DeviceNumber == model.DeviceNumber.ToLower());
            return data != null;
        }

        private Device MapDeviceInputToDevice(DeviceInputModel model)
        {
            return new Device()
            {
                Brand = model.Brand,
                Description = model.Description,
                DeviceNumber = model.DeviceNumber,
                Name = model.Name,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = null,
                CreatedDate = DateTime.Now
            };
        }


        public async Task<DeviceResponseModel> GetDeviceById(Guid deviceId)
        {
            var data = await GetSingleWhere(x => x.DeviceId == deviceId);
            return MapDeviceToDeviceResponse(data);
        }

        private DeviceResponseModel MapDeviceToDeviceResponse(Device model)
        {
            return new DeviceResponseModel()
            {
                DeviceId = model.DeviceId,
                Brand = model.Brand,
                Description = model.Description,
                DeviceNumber = model.DeviceNumber,
                IsActive = model.IsActive,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                
            };

        }

    }
}
