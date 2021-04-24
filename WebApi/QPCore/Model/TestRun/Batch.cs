using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestRun
{
    public class Batch
	{
            public int RunBatchId { set; get; }
            public int ClientId { set; get; }
            public DateTime BatchStartDate { set; get; }
            public DateTime BatchEndDate { set; get; }
            public string RunBy { set; get; }
            public string BatchOutcome { set; get; }
    }
}
