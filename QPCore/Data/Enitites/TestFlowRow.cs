﻿using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class TestFlowRow
    {
        public int RowId { get; set; }
        public int ColumnId { get; set; }
        public string RowValue { get; set; }
        public int? RowNumber { get; set; }
    }
}
