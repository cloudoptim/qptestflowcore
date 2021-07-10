using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.WorkItemTypes
{
    public class CreateWorkItemTypeRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}