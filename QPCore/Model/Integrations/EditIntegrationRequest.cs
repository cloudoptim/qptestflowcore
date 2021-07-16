using System.ComponentModel.DataAnnotations;

namespace QPCore.Model.Integrations
{
    public class EditIntegrationRequest
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(128)]
        public string Organization { get; set; }

        [MaxLength(128)]
        public string Project { get; set; }

        [MaxLength(300)]
        public string Url { get; set; }
    }
}