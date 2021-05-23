using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.TestFlowCategoryAssocs
{
    public class CreateTestFlowCategoryAssocRequest
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int TestFlowId { get; set; }
    }
}