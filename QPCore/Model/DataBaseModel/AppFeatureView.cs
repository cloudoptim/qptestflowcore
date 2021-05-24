using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QPCore.Model.Common.Validations;

namespace QPCore.Model.DataBaseModel
{
    public class AppFeatureView
    {
        public int AppFeatureId { get; set; }
        [Required]
        [IsNotNumber]
        public string FeatureName { get; set; }
        public int? ParentFeatureId { get; set; }
        public int ClientId { get; set; }
        public bool IsActive { get; set; }
        
    }
}
