using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.TestFlowCategories
{
    public class CreateTestFlowCategoryRequest
    {
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        [Required]
        public int ClientId { get; set; }
    }
}