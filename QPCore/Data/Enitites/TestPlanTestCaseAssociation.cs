using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Data.Enitites
{
    public class TestPlanTestCaseAssociation
    {
        public int Id { get; set; }
        public int TestPlanId { get; set; }
        public int TestCaseId { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual TestPlan TestPlan { get; set; }
        public virtual TestFlow TestCase { get; set; }
    }
}
