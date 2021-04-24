using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class RunConfiguration
    {
        public int TestRunConfigId { get; set; }
        public string TestRunConfigName { get; set; }
        public string TestRunKeyValue { get; set; }
        public int RunCofigId { get; set; }
        public string TestRunKeyName { get; set; }
    }
}
