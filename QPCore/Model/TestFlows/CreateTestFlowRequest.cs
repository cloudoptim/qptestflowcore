using QPCore.Model.DataBaseModel.TestFlows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QPCore.Model.TestFlows
{
    public class CreateTestFlowRequest
    {
        public CreateTestFlowRequest()
        {
            this.TestFlowGroup = new TestFlowGroup();
        }

        [JsonPropertyName("steps")]
        public TestFlowGroup TestFlowGroup { get; set; }

        public List<TestFlowStep> UngroupStep()
        {
            var steps = new List<TestFlowStep>();
            if (this.TestFlowGroup != null)
            {
                if (this.TestFlowGroup.DataDriven != null)
                {
                    this.TestFlowGroup.DataDriven.ResourceType = StepResourceType.DATA_DRIVEN;
                    steps.Add(this.TestFlowGroup.DataDriven);
                }

                if (this.TestFlowGroup.DataResource.Any())
                {
                    Parallel.ForEach(this.TestFlowGroup.DataResource, s => s.ResourceType = StepResourceType.DATA_RESOURCE);
                    steps.AddRange(this.TestFlowGroup.DataResource);
                }

                if (this.TestFlowGroup.Step.Any())
                {
                    Parallel.ForEach(this.TestFlowGroup.Step, s => s.ResourceType = StepResourceType.DATA_STEP);
                    steps.AddRange(this.TestFlowGroup.Step);
                }
            }

            return steps;
        }
    }
}
