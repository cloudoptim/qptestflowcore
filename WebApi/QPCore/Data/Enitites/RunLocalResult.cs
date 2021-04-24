using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class RunLocalResult
    {
        public int? Id { get; set; }
        public string TestResults { get; set; }
        public int? Clientid { get; set; }
    }
}
