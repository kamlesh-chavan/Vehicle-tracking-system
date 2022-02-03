using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Core.Model
{
    public class LocationInputModel
    {
        public Guid VehicleId { get; set; }
        public Guid DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
        public LocationDetailsModel Details { get; set; }
    }
}
