using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebModel
    {
        public WebModel()
        {
            WebModelGroups = new HashSet<WebModelGroup>();
        }

        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string FeatureName { get; set; }
        public string ApplicationName { get; set; }
        public bool? Active { get; set; }
        public string Type { get; set; }

        public virtual ICollection<WebModelGroup> WebModelGroups { get; set; }
    }
}
