using VehicleTrackingSystem.Core.Model;

namespace VehicleTrackingSystem.Core.Helper
{
    public static class ValidationHelper
    {
        public static bool IsLocationInputValid(LocationInputModel model)
        {
            if (model == null || model?.VehicleId == null || model?.DeviceId == null)
                return false;

            if (model?.Longitude == null && model?.Latitude == null)
                return false;

            if (model?.Timestamp == null || model?.Timestamp == DateTime.MinValue)
                return false;


            return true;
        }

        public static bool IsLocationSearchInputValid(LocationSearchInputModel model)
        {
            if (model == null || model?.VehicleId == null || model?.DeviceId == null)
                return false;

            if (model?.Longitude == null || !(model?.Longitude == 0) || model?.Latitude == null || !(model?.Latitude == 0))
                return false;

            if (model?.StartTime != null && model?.StartTime == DateTime.MinValue)
                return false;

            if (model?.EndTime != null && model?.EndTime == DateTime.MinValue)
                return false;

            return true;
        }
    }
}
