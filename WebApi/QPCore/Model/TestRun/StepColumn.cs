using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestRun
{
    public class StepColumn
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnIndex { get; set; }
        public int StepId { get; set; }
        public List<StepRow> Rows {get;set;}
    }
}
