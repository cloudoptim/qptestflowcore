using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class ApplicationFeature
    {
        public ApplicationFeature()
        {
            StepGlossaryFeatureAssocs = new HashSet<StepGlossaryFeatureAssoc>();
            Childs = new HashSet<ApplicationFeature>();
        }

        public int AppFeatureId { get; set; }
        public string FeatureName { get; set; }
        public int? ParentFeatureId { get; set; }
        public int? ClientId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ApplicationFeature Parent { get; set; }
        public virtual ICollection<ApplicationFeature> Childs { get; set; }
        public virtual ICollection<StepGlossaryFeatureAssoc> StepGlossaryFeatureAssocs { get; set; }
    }
}
