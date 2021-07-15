using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class TestFlow
    {
        public TestFlow()
        {
            TestFlowCategoryAssocs = new HashSet<TestFlowCategoryAssoc>();
            TestFlowSteps = new HashSet<TestFlowStep>();
            TestPlanTestCaseAssociations = new HashSet<TestPlanTestCaseAssociation>();
            WorkItemTestcaseAssocs = new HashSet<WorkItemTestcaseAssoc>();
        }

        public int TestFlowId { get; set; }
        public string TestFlowName { get; set; }
        public string TestFlowDescription { get; set; }
        public int? LockedBy { get; set; }
        public string TestFlowStatus { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime? AssignedDatetTime { get; set; }
        public int ClientId { get; set; }
        public int? LastUpdatedUserId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public string SourceFeatureName { get; set; }
        public int? SourceFeatureId { get; set; }
        public bool? Islocked { get; set; }
        public bool? IsActive { get; set; }

        public int? AreaId { get; set; }

        public virtual TestFlowCategory Area { get; set; }
        public virtual Application Client { get; set; }
        public virtual ICollection<TestFlowCategoryAssoc> TestFlowCategoryAssocs { get; set; }
        public virtual ICollection<TestFlowStep> TestFlowSteps { get; set; }
        public virtual ICollection<TestPlanTestCaseAssociation> TestPlanTestCaseAssociations { get; set; }
        public virtual ICollection<WorkItemTestcaseAssoc> WorkItemTestcaseAssocs { get; set; }
    }
}
