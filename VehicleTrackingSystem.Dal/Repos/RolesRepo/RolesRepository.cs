using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Dal.Context;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Dal.Repos.RolesRepo
{
    public class RolesRepository : BaseRepository<Role>, IRolesRepository
    {
        public RolesRepository(PostgresDbContext context) : base(context)
        {
        }


        public async Task<Role> GetRoleById(Guid roleId)
        {
            return await GetSingleWhere(x => x.RoleId == roleId && x.IsActive == true && x.IsDeleted == false);
        }
    }
}
