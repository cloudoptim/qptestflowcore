using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestRun
{
    public class TestCase
	{
        public int RunTestCaseId { set; get; }
        public string  TestcaseName { set; get; }
		public string  TestCaseRunStatus { set; get; }
		public DateTime  TestCaseRunStartDate { set; get; }
		public DateTime TestCaseRunEndDate { set; get; }
		public string  TestCaseRunErrorMessage { set; get; }
		public int  TestRunId { set; get; }
		public int configId { set; get; }
		public int TestCaseId { set; get; }
	}
}
