using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.DataBaseModel
{
    public class Row
    {
        public int RowId { get; set; }
        public int ColumnId { get; set; }
        public string RowValue { get; set; }
        public int RowNumber { get; set; }
    }
}
