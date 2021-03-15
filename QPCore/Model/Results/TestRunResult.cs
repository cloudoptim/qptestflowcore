using System;
using System.Collections.Generic;
using System.Text;

namespace QPTestClient.QPFlow.Results
{
    public class TestRunResult
    {
        public TestRunResult()
        {
            TestCaseResults = new List<TestCaseResult>();
        }
       public string Name { set; get; }
       public int clientId { set; get; }
       public TestResult TestResult { set; get; }
       public List<TestCaseResult> TestCaseResults { set; get; }
    }

    public enum ResultType
    {  
       run,
        suite,
        testcase,
        teststep
    }
}
