using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Dal.Entities;

namespace VehicleTrackingSystem.Dal.Repos.RolesRepo
{
    public interface IRolesRepository
    {
        Task<Role> GetRoleById(Guid roleId);
    }
}
