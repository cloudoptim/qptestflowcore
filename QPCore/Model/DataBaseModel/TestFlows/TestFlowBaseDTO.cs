using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.DataBaseModel.TestFlows
{
    public class TestFlowBaseDTO
    {
        public int TestFlowId { get; set; }
        public string TestFlowName { get; set; }
        public string TestFlowDescription { get; set; }
        public int? LockedBy { get; set; }
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
    }
}
