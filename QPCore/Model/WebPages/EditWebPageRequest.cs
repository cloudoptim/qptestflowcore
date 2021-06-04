using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.WebPages
{
    public class EditWebPageRequest : CreateWebPageRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}