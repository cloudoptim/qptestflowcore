using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.TestFlowCategoryAssocs
{
    public class BulkCreateRequest
    {
        public BulkCreateRequest()
        {
            this.TestcaseIdList = new List<int>();
            this.CategoryIdList = new List<int>();
        }
        [Required]
        public List<int> TestcaseIdList { get; set; }

        [Required]
        public List<int> CategoryIdList { get; set; }
    }
}