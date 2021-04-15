using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class RunTestBatch
    {
        public RunTestBatch()
        {
            RunTestRuns = new HashSet<RunTestRun>();
        }

        public int RunBatchId { get; set; }
        public int ClientId { get; set; }
        public DateTime? BatchStartDate { get; set; }
        public DateTime? BatchEndDate { get; set; }
        public string RunBy { get; set; }
        public string BatchOutcome { get; set; }

        public virtual Application Client { get; set; }
        public virtual ICollection<RunTestRun> RunTestRuns { get; set; }
    }
}
