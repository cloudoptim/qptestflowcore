using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Role Role { get; set; }
        public virtual OrgUser OrgUser { get; set; }
    }
}
