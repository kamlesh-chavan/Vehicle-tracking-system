using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Core.Model
{
    public class VehicleDeviceResponseModel
    {
        public Guid VehicleDeviceMapperId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid DeviceId { get; set; }
    }
}
