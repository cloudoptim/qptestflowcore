using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.TestFlowCategories
{
    public class EditTestFlowCategoryRequest : CreateTestFlowCategoryRequest
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]

        public bool IsActive { get; set; }     
    }
}