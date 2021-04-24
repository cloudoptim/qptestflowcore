using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class TestFlowStep
    {
        public TestFlowStep()
        {
            TestFlowColumns = new HashSet<TestFlowColumn>();
        }

        public int TestFlowStepId { get; set; }
        public int StepGlossaryStepId { get; set; }
        public string TestFlowStepName { get; set; }
        public string TestFlowStepDescription { get; set; }
        public string TestFlowStepType { get; set; }
        public string TestFlowStepDataType { get; set; }
        public string TestFlowStepSource { get; set; }
        public int ClientId { get; set; }
        public int TestFlowId { get; set; }
        public bool? IsActive { get; set; }
        public int? OrderNumber { get; set; }
        public string ResourceType { get; set; }

        public virtual TestFlow TestFlow { get; set; }
        public virtual ICollection<TestFlowColumn> TestFlowColumns { get; set; }
    }
}
