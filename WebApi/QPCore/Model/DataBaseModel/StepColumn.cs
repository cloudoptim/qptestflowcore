using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.DataBaseModel
{
    public class Column
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnIndex { get; set; }
        public int StepId { get; set; }
        public Row[] Rows { get; set; }
    }
}
