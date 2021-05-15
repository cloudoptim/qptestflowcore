using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.TestPlanTestCases
{
    public class TestPlanTestCaseResponse
    {
        public int Id { get; set; }
        public int TestPlanId { get; set; }
        public string TestPlanName { get; set; }
        public int TestCaseId { get; set; }
        public string TestCaseName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
