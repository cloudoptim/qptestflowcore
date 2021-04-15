using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class ConfigTestFlowConfig
    {
        public ConfigTestFlowConfig()
        {
            ConfigTestFlowConfigValues = new HashSet<ConfigTestFlowConfigValue>();
        }

        public int ConfigId { get; set; }
        public int ClientId { get; set; }
        public string ConfigName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSystemDefined { get; set; }

        public virtual Application Client { get; set; }
        public virtual ICollection<ConfigTestFlowConfigValue> ConfigTestFlowConfigValues { get; set; }
    }
}
