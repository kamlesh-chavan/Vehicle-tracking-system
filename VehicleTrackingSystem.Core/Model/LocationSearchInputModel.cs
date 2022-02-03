namespace VehicleTrackingSystem.Core.Model
{
    public class LocationSearchInputModel
    {
        public Guid VehicleId { get; set; }
        public Guid DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool GetCurrentLocation { get; set; }
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;

    }
}
