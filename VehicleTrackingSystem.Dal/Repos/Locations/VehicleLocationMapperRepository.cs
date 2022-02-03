using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Text.Json;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Context;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Dal.Repos.Locations
{
    public class VehicleLocationMapperRepository : BaseRepository<VehicleLocationMapper>, IVehicleLocationMapperRepository
    {
        public VehicleLocationMapperRepository(PostgresDbContext context) : base(context)
        {
        }

        public async Task<int> AddLocation(LocationInputModel model, Guid vehicleDeviceId)
        {
            var location = MapLocationInputToVehicleLocationMapper(model, vehicleDeviceId);
            await AddAsync(location);
            return await CommitAsync();
        }

        public async Task<LocationResponseModel> GetLocation(LocationSearchInputModel model)
        {
            var data = await GetManyWhere(x => x.Lat == model.Latitude && x.Long == model.Longitude 
                                        && x.Timestamp >= model.StartTime && x.Timestamp <= model.EndTime
                                        && x.VehicleDeviceMapper.VehicleId == model.VehicleId && x.VehicleDeviceMapper.DeviceId == model.DeviceId);
            return MapVehicleLocationMapperToLocation(data)
                    ?.OrderBy(x => x.TimeStamp)
                    ?.FirstOrDefault();
        }

        public async Task<IEnumerable<LocationResponseModel>> GetLocations(LocationSearchInputModel model)
        {
            var data = GetAllVehicleLocations(model);
            return MapVehicleLocationMapperToLocation(data);
        }

        public async Task<IEnumerable<LocationResponseModel>> GetLocationsByVehicleAndDeviceId(Guid vehicleId, Guid deviceId)
        {
            var data = await GetManyWhere(x => x.VehicleDeviceMapper.VehicleId == vehicleId && x.VehicleDeviceMapper.DeviceId == deviceId);
            return MapVehicleLocationMapperToLocation(data);
        }

        public async Task<IEnumerable<LocationResponseModel>> GetLocations(Guid vehicleDeviceId)
        {
            var data = await GetManyWhere(x => x.VehicleDeviceMapperId == vehicleDeviceId);
            return MapVehicleLocationMapperToLocation(data);
        }

        public IQueryable<VehicleLocationMapper> GetAllVehicleLocations(LocationSearchInputModel model)
        {
            var data = All();

            if (model.VehicleId != Guid.Empty && model.DeviceId != Guid.Empty)
                data = data.Where(x => x.VehicleDeviceMapper.VehicleId == model.VehicleId && x.VehicleDeviceMapper.DeviceId == model.DeviceId);

            if (model.Latitude > 0 && model.Longitude > 0)
                data = data.Where(x => x.Lat == model.Latitude && x.Long == model.Longitude);

            if (model.GetCurrentLocation)
            {
                var date = DateTime.Now.ToUniversalTime().AddSeconds(-30);
                data = data.Where(x => x.Timestamp.ToUniversalTime().CompareTo(date) > 0 );
            }
            else
            {
                if (model.StartTime != null && model.StartTime != DateTime.MinValue)
                    data = data.Where(x => x.Timestamp >= model.StartTime);

                if (model.EndTime != null && model.EndTime != DateTime.MinValue)
                    data = data.Where(x => x.Timestamp <= model.EndTime);
            }
            return data.OrderByDescending(x=> x.Timestamp);
        }
        private VehicleLocationMapper MapLocationInputToVehicleLocationMapper(LocationInputModel model, Guid vehicleDeviceId)
        {
            return new VehicleLocationMapper()
            {
                VehicleDeviceMapperId = vehicleDeviceId,
                Lat = model.Latitude,
                Long = model.Longitude,
                Details = model?.Details != null ? JsonConvert.SerializeObject(model.Details) : null,
                Timestamp = DateTime.Now
            };
        }

        private IEnumerable<LocationResponseModel> MapVehicleLocationMapperToLocation(IQueryable<VehicleLocationMapper> model)
        {
            var locations = new ConcurrentBag<LocationResponseModel>();

            Parallel.ForEach(model, item =>
            {
                locations.Add(new LocationResponseModel()
                {
                    Latitude = item.Lat,
                    Longitude = item.Long,
                    Details = !string.IsNullOrEmpty(item?.Details) ? JsonConvert.DeserializeObject<LocationDetailsModel>(item.Details) : new LocationDetailsModel(),
                    TimeStamp = item.Timestamp
                });
            });

            return locations;
        }

    }
}
