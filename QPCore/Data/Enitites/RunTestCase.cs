using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class RunTestCase
    {
        public RunTestCase()
        {
            RunTestColumns = new HashSet<RunTestColumn>();
        }

        public int RunTestCaseId { get; set; }
        public string TestcaseName { get; set; }
        public string TestCaseRunStatus { get; set; }
        public DateTime? TestCaseRunStartDate { get; set; }
        public DateTime? TestCaseRunEndDate { get; set; }
        public string TestCaseRunErrorMessage { get; set; }
        public int? TestRunId { get; set; }
        public int? ConfigId { get; set; }
        public int? TestCaseId { get; set; }

        public virtual RunTestRun TestRun { get; set; }
        public virtual ICollection<RunTestColumn> RunTestColumns { get; set; }
    }
}
