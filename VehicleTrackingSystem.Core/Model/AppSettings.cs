using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Core.Model
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string GoogleGeocodeReverseApi { get; set; }
        public string GoogleKey { get; set; }
        public int TokenExpiryTimeInMinutes { get; set; }
        public int BearerTokenValidityInMinutes { get; set; }

    }
}
