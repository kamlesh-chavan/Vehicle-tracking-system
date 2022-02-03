using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Bal
{
    public interface IUserService
    {
        Task<User> GetUser(AuthenticateRequest model);
    }
}
