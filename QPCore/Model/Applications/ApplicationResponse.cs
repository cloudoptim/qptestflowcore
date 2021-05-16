using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Applications
{
    public class ApplicationResponse
    {
        public int ClientId { get; set; }
        public int ApplicationId { get; set; }
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public string ApplicationName { get; set; }
        public bool? IsActive { get; set; }
    }
}
