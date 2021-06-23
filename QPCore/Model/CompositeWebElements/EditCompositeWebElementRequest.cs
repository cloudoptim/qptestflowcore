using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QPCore.Model.Common.Validations;

namespace QPCore.Model.CompositeWebElements
{
    public class EditCompositeWebElementRequest
    {
        public EditCompositeWebElementRequest()
        {
            this.Childs = new List<EditChildCompositeWebElementRequest>();
        }
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [IsNotNumber]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("pageId")]
        public int GroupId { get; set; }
        
        public List<EditChildCompositeWebElementRequest> Childs { get; set; }
    }
}