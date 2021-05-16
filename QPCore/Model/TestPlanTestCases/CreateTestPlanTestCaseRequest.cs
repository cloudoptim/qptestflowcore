using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestPlanTestCases
{
    public class CreateTestPlanTestCaseRequest
    {
        [Required]
        public int TestPlanId { get; set; }

        [Required]
        public List<int> TestCaseIds { get; set; }
    }
}
