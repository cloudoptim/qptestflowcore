using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class ConfigTestFlowConfigValue
    {
        public int PairId { get; set; }
        public int? ConfigId { get; set; }
        public string KeyName { get; set; }
        public string KeyValue { get; set; }

        public virtual ConfigTestFlowConfig Config { get; set; }
    }
}
