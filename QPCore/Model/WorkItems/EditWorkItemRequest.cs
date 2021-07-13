using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.WorkItems
{
    public class EditWorkItemRequest : CreateWorkItemRequest
    {
        [Required]
        public int Id { get; set; }
    }
}