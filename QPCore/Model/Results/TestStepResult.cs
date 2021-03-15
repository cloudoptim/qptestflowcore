using QPCore.Model.DataBaseModel.TestFlows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QPTestClient.QPFlow.Results
{
    public class TestStepResult
    {
        public TestFlowStep TestStep { set; get; }
        public TestResult TestResult { set; get; }
    }
}
