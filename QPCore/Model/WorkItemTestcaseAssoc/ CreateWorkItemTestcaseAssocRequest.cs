using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.WorkItemTestcaseAssoc
{
    public class  CreateWorkItemTestcaseAssocRequest
    {   
        [Required]
        public int WorkItemId { get; set; }

        [Required]
        public int TestcaseId { get; set; }
    }
}