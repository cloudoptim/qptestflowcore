using System.ComponentModel.DataAnnotations;
using QPCore.Model.Common.Validations;

namespace QPCore.Model.TestFlowCategories
{
    public class CreateTestFlowCategoryRequest
    {
        [Required]
        [MaxLength(100)]
        [IsNotNumber]
        public string CategoryName { get; set; }

        [Required]
        public int ClientId { get; set; }
    }
}