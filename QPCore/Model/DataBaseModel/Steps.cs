
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.DataBaseModel
{
    public class Steps
    {
        public int StepId { get; set; }
        public string StepName { get; set; }
        public string StepDescription { get; set; }
        public string StepType { get; set; }
        public string StepDataType { get; set; }
        public string DisplayStepName { get; set; }
        public string StepSource { get; set; }
        public bool IsActive { get; set; }
        public int ClientId { get; set; }
        public int FeatureId { get; set; }
        public List<Column> Columns { get; set; }
    }
}
