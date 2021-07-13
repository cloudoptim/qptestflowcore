using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.WorkItemTypes
{
    public class EditWorkItemTypeRequest : CreateWorkItemTypeRequest
    {
        [Required]
        public int Id { get; set; }
    }
}