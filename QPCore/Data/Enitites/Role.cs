using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class Role
    {
        public Role()
        {
            this.UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleCode { get; set; }
        public string Rolename { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        public bool IsDefault { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
