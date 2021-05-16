using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestPlans
{
    public class EditTestPlanRequest : CreateTestPlanRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}
