using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestRun
{
    public class Run
    {
        public int RunId { set; get; }
        public int Batchid { set; get; }
        public string ApplicationName { set; get; }
        public string RunStatus { set; get; }
        public DateTime RunStartDate { set; get; }
        public DateTime RunEndDate { set; get; }
        public string RuntStdouput { set; get; }
    }
}
