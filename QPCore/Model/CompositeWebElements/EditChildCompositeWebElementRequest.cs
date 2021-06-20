using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.CompositeWebElements
{
    public class EditChildCompositeWebElementRequest : CreateChildCompositeWebElementRequest
    {
        [Required]
        public int Id { get; set; }
    }
}