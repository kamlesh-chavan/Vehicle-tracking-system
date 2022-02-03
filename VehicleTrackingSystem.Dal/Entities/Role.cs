using System;
using System.Collections.Generic;

namespace VehicleTrackingSystem.Dal.Entities
{
    public partial class Role
    {
        public Role()
        {
            UsersPermissionMappers = new HashSet<UsersPermissionMapper>();
            UsersRolesMappers = new HashSet<UsersRolesMapper>();
        }

        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<UsersPermissionMapper> UsersPermissionMappers { get; set; }
        public virtual ICollection<UsersRolesMapper> UsersRolesMappers { get; set; }
    }
}
