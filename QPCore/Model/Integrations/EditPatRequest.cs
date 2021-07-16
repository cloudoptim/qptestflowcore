using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.Integrations
{
    public class EditPatRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Pat { get; set; }
    }
}