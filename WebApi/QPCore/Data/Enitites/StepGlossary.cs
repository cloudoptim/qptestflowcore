using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class StepGlossary
    {
        public StepGlossary()
        {
            StepGlossaryColumns = new HashSet<StepGlossaryColumn>();
            StepGlossaryFeatureAssocs = new HashSet<StepGlossaryFeatureAssoc>();
        }

        public int StepId { get; set; }
        public string StepName { get; set; }
        public string StepDescription { get; set; }
        public string StepType { get; set; }
        public string StepDataType { get; set; }
        public string DisplayStepName { get; set; }
        public string StepSource { get; set; }
        public int ClientId { get; set; }
        public int? FeatureId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Application Client { get; set; }
        public virtual ICollection<StepGlossaryColumn> StepGlossaryColumns { get; set; }
        public virtual ICollection<StepGlossaryFeatureAssoc> StepGlossaryFeatureAssocs { get; set; }
    }
}
