using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QPCore.Model.WorkItems
{
    public class CreateWorkItemRequest
    {
        [Required]
        public int AzureWorkItemId { get; set; }

        [Required]
        [MaxLength(500)]
        [JsonPropertyName("azureWorkItemName")]
        public string Name { get; set; }

        public int AzureFeatureId { get; set; }

        [Required]
        public int WorkItemTypeId { get; set; }

        [MaxLength(500)]
        public string AzureFeatureName { get; set; }
    }
}