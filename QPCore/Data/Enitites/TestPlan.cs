using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Data.Enitites
{
    public class TestPlan
    {
        public TestPlan()
        {
            TestPlanTestCaseAssociations = new HashSet<TestPlanTestCaseAssociation>();
            Childs = new HashSet<TestPlan>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }
        public int AssignTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual OrgUser OrgUser { get; set; }
        public virtual TestPlan Parent { get; set; }
        public virtual ICollection<TestPlan> Childs { get; set; }
        public virtual ICollection<TestPlanTestCaseAssociation> TestPlanTestCaseAssociations { get; set; }
    }
}
