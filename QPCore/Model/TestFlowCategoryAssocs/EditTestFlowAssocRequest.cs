using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.TestFlowCategoryAssocs
{
    public class EditTestFlowAssocRequest : CreateTestFlowCategoryAssocRequest
    {
        [Required]
        public int TestFlowCatAssocId { get; set; }
    }
}