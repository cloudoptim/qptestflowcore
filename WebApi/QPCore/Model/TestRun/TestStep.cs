using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestRun
{
    public class TestStep
    {
        public int TestStepId { set; get; }
        public string TestStepName { set; get; }
        public DateTime TestStepStartDate { set; get; }
        public DateTime TestStepEndDate { set; get; }
        public string StepStatus { set; get; }
        public string TestStepErrorMessage { set; get; }
        public string TextMetaData { set; get; }
        public int RunTestCaseId { set; get; }
        public int RunStepId { set; get; }
        public List<StepColumn> Columns {set;get;}
        
    }
}
