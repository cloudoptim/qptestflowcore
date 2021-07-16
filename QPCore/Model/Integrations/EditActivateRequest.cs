using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.Integrations
{
    public class EditActivateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}