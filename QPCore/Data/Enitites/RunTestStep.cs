using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class RunTestStep
    {
        public int TestStepId { get; set; }
        public string TestStepName { get; set; }
        public DateTime? TestStepStartDate { get; set; }
        public DateTime? TestStepEndDate { get; set; }
        public string StepStatus { get; set; }
        public string TestStepErrorMessage { get; set; }
        public string TextMetaData { get; set; }
        public int? RunTestCaseId { get; set; }
        public int? RunStepId { get; set; }
    }
}
