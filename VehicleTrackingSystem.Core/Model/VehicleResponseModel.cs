using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Core.Model
{
    public class VehicleResponseModel
    {
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
    }
}
