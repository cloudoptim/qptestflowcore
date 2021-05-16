using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class Organization
    {
        public Organization()
        {
            Applications = new HashSet<Application>();
            OrgUsers = new HashSet<OrgUser>();
        }

        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<OrgUser> OrgUsers { get; set; }
    }
}
