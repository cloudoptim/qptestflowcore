using System;
using System.Collections.Generic;
using System.Text;

namespace QPTestClient.QPFlow.Results
{
    public class TestCaseResult
    {
        public TestCaseResult()
        {
            TestStepResults = new List<TestStepResult>();
        }
        public string Name { set; get; }
        public TestResult TestResult { set; get; }
        public List<TestStepResult> TestStepResults { set; get; }
    }
}
