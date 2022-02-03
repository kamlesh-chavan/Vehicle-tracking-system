using System;
using System.Collections.Generic;

namespace VehicleTrackingSystem.Dal.Entities
{
    public partial class UsersPermissionMapper
    {
        public Guid UsersPermissionMapperId { get; set; }
        public Guid PermissionId { get; set; }
        public Guid RoleId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
