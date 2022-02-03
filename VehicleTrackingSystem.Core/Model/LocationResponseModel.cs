using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Core.Model
{
    public class LocationResponseModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Locality { get; set; }
        public DateTime? TimeStamp { get; set; } = null;
        public LocationDetailsModel Details { get; set; } = null;
    }
}
