using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestPlans
{
    public class CreateTestPlanRequest
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AssignTo { get; set; }


    }
}
