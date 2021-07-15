using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.DataBaseModel.TestFlows
{
    public class TestFlowBaseDTO
    {
        public int TestFlowId { get; set; }

        [Required]
        [MaxLength(250)]
        public string TestFlowName { get; set; }

        [MaxLength(2000)]
        public string TestFlowDescription { get; set; }

        public int? LockedBy { get; set; }

        [MaxLength(50)]
        [RegularExpression("Draft|Ready|Template|Active")]
        public string TestFlowStatus { get; set; }

        public int AreaId { get; set; }

        public string AreaName { get; set; }

        public int AssignedTo { get; set; }
        
        public string AssignedDatetTime { get; set; }
        
        public int ClientId { get; set; }
        public string ApplicationName { get; set; }
        
        public int LastUpdatedUserId { get; set; }
        
        public string LastUpdatedDateTime { get; set; }
        
        [MaxLength(1000)]
        public string SourceFeatureName { get; set; }
        
        public int SourceFeatureId { get; set; }
        
        public bool Islocked { get; set; }
        
        public bool IsActive { get; set; }
    }
}
