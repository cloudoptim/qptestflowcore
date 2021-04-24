using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class TestFlowColumn
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int? ColumnIndex { get; set; }
        public int? TestFlowStepId { get; set; }

        public virtual TestFlowStep TestFlowStep { get; set; }
    }
}
