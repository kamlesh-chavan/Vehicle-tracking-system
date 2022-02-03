using System;
using System.Collections.Generic;

namespace VehicleTrackingSystem.Dal.Entities
{
    public partial class UsersRolesMapper
    {
        public Guid UsersRolesMapperId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
