using QPTestClient.QPFlow.Results.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace QPTestClient.QPFlow.Results
{
    public  class TestResult
    {
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public string ErrorMessage { set; get; }
        public string SatckTrace { set; get; }
        public string Status { set; get; }
        public string ExecutionTime { get; set; }
        public string Screenshot { get; set; }
    }
}
