using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.WorkItemTestcaseAssoc
{
    public class  EditWorkItemTestcaseAssocRequest : CreateWorkItemTestcaseAssocRequest
    {   
        [Required]
        public int Id { get; set; }
    }
}