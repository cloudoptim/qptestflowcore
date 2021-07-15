using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.Integrations
{
    public class CreateIntegrationRequest
    {
        [Required]
        public int SourceId { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Pat { get; set; }

        
        public string Organization { get; set; }

        public string Project { get; set; }

        public string Url { get; set; }

        
    }
}