using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.CompositeWebElements
{
    public class EditCompositeWebElementRequest : CreateCompositeWebElementRequest
    {
        [Required]
        public int Id { get; set; }
    }
}