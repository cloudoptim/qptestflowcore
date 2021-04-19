using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class StepGlossaryFeatureAssoc
    {
        public int FeatureAssocId { get; set; }
        public int Featureid { get; set; }
        public int StepId { get; set; }
        public BitArray IsActive { get; set; }

        public virtual ApplicationFeature Feature { get; set; }
        public virtual StepGlossary Step { get; set; }
    }
}
