using System;
using System.Collections.Generic;

namespace VehicleTrackingSystem.Dal.Entities
{
    public partial class Device
    {
        public Device()
        {
            VehicleDeviceMappers = new HashSet<VehicleDeviceMapper>();
        }

        public Guid DeviceId { get; set; }
        public string DeviceNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<VehicleDeviceMapper> VehicleDeviceMappers { get; set; }
    }
}
