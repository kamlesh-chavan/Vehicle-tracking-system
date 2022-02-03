using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Bal.Services.LocationService;
using VehicleTrackingSystem.Core.Http;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Context;
using VehicleTrackingSystem.Dal.Repos.Locations;
using VehicleTrackingSystem.Dal.Repos.VehicleDevice;

namespace VehicleTrackingSystem.Bal.Tests.Services
{
    [TestFixture]

    public class LocationServiceTests
    {

        private Mock<PostgresDbContext> dbContext;
        Mock<IVehicleLocationMapperRepository> vehicleLocationMapperRepository = new Mock<IVehicleLocationMapperRepository>();
        Mock<IVehicleDeviceRepository> vehicleDeviceRepository = new Mock<IVehicleDeviceRepository>();
        Mock<IHttpClientAdapter> httpClientAdapter = new Mock<IHttpClientAdapter>();
        Mock<IOptions<AppSettings>> constants = new Mock<IOptions<AppSettings>>();

        [SetUp]
        public void Setup()
        {
        }

        
        [TestCase()]
        public void AddLocation_Test()
        {
            var location = new LocationInputModel();
            location.VehicleId = Guid.NewGuid();
            location.DeviceId = Guid.NewGuid();
            location.Latitude = 74.1466;
            location.Longitude = 94.8478;
            location.Timestamp = DateTime.Now;
            
            var suit = new LocationService(vehicleLocationMapperRepository.Object, vehicleDeviceRepository.Object, 
                httpClientAdapter.Object, constants.Object);
            var data = suit.AddLocation(location);

            Assert.IsNotNull(data);
            Assert.NotZero(data.Id);
        }
    }
}
