using System;
using System.Collections.Generic;

namespace VehicleTrackingSystem.Dal.Entities
{
    public partial class User
    {
        public User()
        {
            UsersRolesMappers = new HashSet<UsersRolesMapper>();
        }

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<UsersRolesMapper> UsersRolesMappers { get; set; }
    }
}
