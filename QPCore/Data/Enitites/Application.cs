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
            AppUsers = new HashSet<AppUser>();
        }

        public int ClientId { get; set; }
        public int OrgId { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Organization Org { get; set; }
        public virtual ICollection<ConfigTestFlowConfig> ConfigTestFlowConfigs { get; set; }
        public virtual ICollection<RunTestBatch> RunTestBatches { get; set; }
        public virtual ICollection<StepGlossary> StepGlossaries { get; set; }
        public virtual ICollection<TestFlowCategory> TestFlowCategories { get; set; }
        public virtual ICollection<TestFlow> TestFlows { get; set; }
        public virtual ICollection<WebCommand> WebCommands { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
