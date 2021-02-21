using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.DataBaseModel
{
    public class Child
    {
        public int AppFeatureId { get; set; }
        public string FeatureName { get; set; }
        public int ParentFeatureId { get; set; }
        public int ClientId { get; set; }
        public bool IsActive { get; set; }
    }
}
