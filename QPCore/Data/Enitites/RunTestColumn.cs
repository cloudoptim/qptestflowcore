using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class RunTestColumn
    {
        public RunTestColumn()
        {
            RunTestRows = new HashSet<RunTestRow>();
        }

        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int? ColumnIndex { get; set; }
        public int? StepId { get; set; }

        public virtual RunTestCase Step { get; set; }
        public virtual ICollection<RunTestRow> RunTestRows { get; set; }
    }
}
