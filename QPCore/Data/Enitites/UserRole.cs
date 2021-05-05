using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class UserRole
    {
        public int ClientRoleAssoc { get; set; }
        public int UserClientId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }

        public virtual Role Role { get; set; }
        public virtual OrgUser OrgUser { get; set; }
    }
}
