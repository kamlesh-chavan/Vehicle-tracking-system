using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Entities;
using VehicleTrackingSystem.Dal.Repos.Users;

namespace VehicleTrackingSystem.Bal
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository usersRepository;
        public UserService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }
        public async Task<User> GetUser(AuthenticateRequest model)
        {
            return await this.usersRepository.GetUserByEmailAndPassword(model);
        }
    }
}
