using Microsoft.EntityFrameworkCore;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Context;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Dal.Repos.Users
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(PostgresDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByEmailAndPassword(AuthenticateRequest model)
        {
            return GetManyWhereWithIncludes(x => x.EmailId.ToLower() == model.Email.ToLower() && x.Password == model.Password && x.IsActive == true && x.IsDeleted == false)
                .Include(x => x.UsersRolesMappers)
                .FirstOrDefault(); ;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await GetSingleWhere(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false);
        }
    }
}
