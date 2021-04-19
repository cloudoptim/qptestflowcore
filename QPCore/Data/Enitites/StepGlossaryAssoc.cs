using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class StepGlossaryAssoc
    {
        public int StepAssocId { get; set; }
        public int Stepid { get; set; }
        public int ParentStepId { get; set; }
    }
}
