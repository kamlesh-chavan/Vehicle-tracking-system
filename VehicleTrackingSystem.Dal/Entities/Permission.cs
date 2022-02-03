using System;
using System.Collections.Generic;

namespace VehicleTrackingSystem.Dal.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            UsersPermissionMappers = new HashSet<UsersPermissionMapper>();
        }

        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<UsersPermissionMapper> UsersPermissionMappers { get; set; }
    }
}
