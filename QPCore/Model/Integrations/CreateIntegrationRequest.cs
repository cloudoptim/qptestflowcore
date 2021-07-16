using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QPCore.Model.Integrations
{
    public class CreateIntegrationRequest
    {
        public CreateIntegrationRequest()
        {
            this.IsActive = true;
        }
        public bool IsActive { get; internal set; } 

        [Required]
        public int SourceId { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Pat { get; set; }

        [MaxLength(128)]
        public string Organization { get; set; }

        [MaxLength(128)]
        public string Project { get; set; }

        [MaxLength(300)]
        public string Url { get; set; }

        
    }
}