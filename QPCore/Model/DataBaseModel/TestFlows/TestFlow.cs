using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QPCore.Model.DataBaseModel.TestFlows
{
    public class StepResourceType
    {
        public const string DATA_DRIVEN = "Data Driven";
        public const string DATA_RESOURCE = "Data Resource";
        public const string DATA_STEP = "Data Step";
    }

    public class TestFlow : TestFlowBaseDTO
    {
        public TestFlow()
        {
            this.Steps = new List<TestFlowStep>();
        }
        public List<TestFlowStep> Steps { get; set; }

        public TestFlowGroup GroupStep()
        {
            var testFlowGroup = new TestFlowGroup();
            if (this.Steps.Any())
            {
                testFlowGroup.DataDriven = this.Steps.FirstOrDefault(s => s.ResourceType == StepResourceType.DATA_DRIVEN);
                testFlowGroup.DataResource = this.Steps.Where(s => s.ResourceType == StepResourceType.DATA_RESOURCE).ToList();
                testFlowGroup.Step = this.Steps.Where(s => s.ResourceType == StepResourceType.DATA_STEP || (s.ResourceType != StepResourceType.DATA_DRIVEN && s.ResourceType != StepResourceType.DATA_RESOURCE)).ToList();
            }

            return testFlowGroup;
        }
    }

    public class TestFlowGroup
    {
        public TestFlowGroup()
        {
            this.Step = new List<TestFlowStep>();
            this.DataResource = new List<TestFlowStep>();
        }
        public TestFlowStep DataDriven { get; set; }

        public List<TestFlowStep> Step { get; set; }

        public List<TestFlowStep> DataResource { get; set; }
    }

    public class TestFlowStep
    {
        public TestFlowStep()
        {
            this.Columns = new List<TestFlowStepColumn>();
        }
        public int TestFlowId { get; set; }
        public int TestFlowStepId { get; set; }
        public int StepGlossaryStepId { get; set; }
        public string TestFlowStepName { get; set; }
        public string TestFlowStepDescription { get; set; }
        public string TestFlowStepType { get; set; }
        public string TestFlowStepDataType { get; set; }
        public string TestFlowStepSource { get; set; }
        public bool IsActive { get; set; }
        public int OrderNumber { get; set; }
        public int ClientId { get; set; }
        public string ResourceType { get; set; }

        public List<TestFlowStepColumn> Columns { get; set; }
    }

    public class TestFlowStepColumn
    {
        public TestFlowStepColumn()
        {
            this.Rows = new List<TestFlowStepRow>();
        }
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
