using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Common
{
    public class CheckLockingDTO
    {
        public bool IsLocked { get; set; }
        public string LockedByName { get; set; }
        public int? LockedById { get; set; }
        public string LockedByEmail { get; set; }
    }
}
