﻿using System;
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

        public int Orgid { get; set; }
        public string OrgName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<OrgUser> OrgUsers { get; set; }
    }
}
