using System;
using System.Collections.Generic;

namespace VehicleTrackingSystem.Dal.Entities
{
    public partial class VehicleLocationMapper
    {
        public Guid VehicleLocationMapperId { get; set; }
        public Guid VehicleDeviceMapperId { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual VehicleDeviceMapper VehicleDeviceMapper { get; set; }
    }
}
