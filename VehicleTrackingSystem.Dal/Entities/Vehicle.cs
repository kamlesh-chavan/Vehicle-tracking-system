using System;
using System.Collections.Generic;

namespace VehicleTrackingSystem.Dal.Entities
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            VehicleDeviceMappers = new HashSet<VehicleDeviceMapper>();
        }

        public Guid VehicleId { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public string ModelNumber { get; set; }
        public string Year { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<VehicleDeviceMapper> VehicleDeviceMappers { get; set; }
    }
}
