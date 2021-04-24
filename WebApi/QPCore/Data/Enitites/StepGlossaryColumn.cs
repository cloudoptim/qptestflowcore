using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class StepGlossaryColumn
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int? ColumnIndex { get; set; }
        public int? StepId { get; set; }

        public virtual StepGlossary Step { get; set; }
    }
}
