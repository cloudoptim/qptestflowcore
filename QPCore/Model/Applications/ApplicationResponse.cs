using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.Applications
{
    public class ApplicationResponse
    {
        public int ClientId { get; set; }
        public string ApplicationName { get; set; }
        public bool? IsActive { get; set; }
        public bool Enable { get; set; }
    }
}
