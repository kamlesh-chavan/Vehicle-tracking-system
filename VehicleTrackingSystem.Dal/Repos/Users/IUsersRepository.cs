using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Dal.Repos.Users
{
    public interface IUsersRepository
    {
        Task<User> GetUserByEmailAndPassword(AuthenticateRequest model);

        Task<User> GetUserById(Guid userId);
    }
}
