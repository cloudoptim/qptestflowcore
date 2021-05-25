using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace QPCore.Model.WebElement
{
    public class WebElementItem
    {
        [JsonProperty("elementid")]
        public int? ElementId { get; set; }

        [Required]
        [MaxLength(550)]
        [JsonProperty("elementaliasname")]
        public string ElementAliasName { get; set; }
    }
}