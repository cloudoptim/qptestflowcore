using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.DataBaseModel.TestFlows
{

    public class TestFlow
    {
        public int TestFlowId { get; set; }
        public string TestFlowName { get; set; }
        public string TestFlowDescription { get; set; }
        public int LockedBy { get; set; }
        public string TestFlowStatus { get; set; }
        public int AssignedTo { get; set; }
        public string AssignedDatetTime { get; set; }
        public int ClientId { get; set; }
        public int LastUpdatedUserId { get; set; }
        public string LastUpdatedDateTime { get; set; }
        public string SourceFeatureName { get; set; }
        public int SourceFeatureId { get; set; }
        public bool Islocked { get; set; }
        public bool IsActive { get; set; }
        public List<TestFlowStep> Steps { get; set; }
    }

    public class TestFlowStep
    {
        public int TestFlowId { get; set; }
        public int TestFlowStepId { get; set; }
        public int StepGlossaryStepId { get; set; }
        public string TestFlowStepName { get; set; }
        public string TestFlowStepDescription { get; set; }
        public string TestFlowStepType { get; set; }
        public string TestFlowStepDataType { get; set; }
        public string TestFlowStepSource { get; set; }
        public bool IsActive { get; set; }
        public int ClientId { get; set; }
        public List<TestFlowStepColumn> Columns { get; set; }
    }

    public class TestFlowStepColumn
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnIndex { get; set; }
        public int TestFlowStepId { get; set; }
        public List<TestFlowStepRow> Rows { get; set; }
    }

    public class TestFlowStepRow
    {
        public int RowId { get; set; }
        public int ColumnId { get; set; }
        public string RowValue { get; set; }
        public int RowNumber { get; set; }
    }

}
