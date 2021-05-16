using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestPlans
{
    public class TestPlanResponse : EditTestPlanRequest
    {
        public string ParentName { get; set; }

        public string AssignToFirstName { get; set; }

        public string AssignToLastName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
