using System;
using System.Collections.Generic;

namespace VehicleTrackingSystem.Dal.Entities
{
    public partial class VehicleDeviceMapper
    {
        public VehicleDeviceMapper()
        {
            VehicleLocationMappers = new HashSet<VehicleLocationMapper>();
        }

        public Guid VehicleDeviceMapperId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid DeviceId { get; set; }

        public virtual Device Device { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<VehicleLocationMapper> VehicleLocationMappers { get; set; }
    }
}
