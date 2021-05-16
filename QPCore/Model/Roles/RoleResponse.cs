using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Roles
{
    public class RoleResponse
    {
        public int RoleId { get; set; }
        public string Rolename { get; set; }
        public string RoleCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        public bool IsDefault { get; set; }
    }
}
