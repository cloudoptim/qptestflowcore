using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class RunTestRun
    {
        public RunTestRun()
        {
            RunTestCases = new HashSet<RunTestCase>();
        }

        public int RunId { get; set; }
        public int Batchid { get; set; }
        public string ApplicationName { get; set; }
        public string RunStatus { get; set; }
        public DateTime? RunStartDate { get; set; }
        public DateTime? RunEndDate { get; set; }
        public string RuntStdouput { get; set; }

        public virtual RunTestBatch Batch { get; set; }
        public virtual ICollection<RunTestCase> RunTestCases { get; set; }
    }
}
