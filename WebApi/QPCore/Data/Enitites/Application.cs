using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class Application
    {
        public Application()
        {
            ConfigTestFlowConfigs = new HashSet<ConfigTestFlowConfig>();
            RunTestBatches = new HashSet<RunTestBatch>();
            StepGlossaries = new HashSet<StepGlossary>();
            TestFlowCategories = new HashSet<TestFlowCategory>();
            TestFlows = new HashSet<TestFlow>();
            WebCommands = new HashSet<WebCommand>();
        }

        public int ClientId { get; set; }
        public int Orgid { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public bool? IsActive { get; set; }

        public virtual Organization Org { get; set; }
        public virtual ICollection<ConfigTestFlowConfig> ConfigTestFlowConfigs { get; set; }
        public virtual ICollection<RunTestBatch> RunTestBatches { get; set; }
        public virtual ICollection<StepGlossary> StepGlossaries { get; set; }
        public virtual ICollection<TestFlowCategory> TestFlowCategories { get; set; }
        public virtual ICollection<TestFlow> TestFlows { get; set; }
        public virtual ICollection<WebCommand> WebCommands { get; set; }
    }
}
