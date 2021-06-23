using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using QPCore.Model.Common.Validations;

namespace QPCore.Model.CompositeWebElements
{
    public class CreateCompositeWebElementRequest
    {
        public CreateCompositeWebElementRequest()
        {
            this.Childs = new List<CreateChildCompositeWebElementRequest>();
        }
        
        [Required]
        [MaxLength(255)]
        [IsNotNumber]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("pageId")]
        public int GroupId { get; set; }
        
        public List<CreateChildCompositeWebElementRequest> Childs { get; set; }
    }
}