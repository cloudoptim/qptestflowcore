using System.Text.Json.Serialization;

namespace QPCore.Model.WebElement
{
    public class CheckingWebElementItem
    {
        public string PageId { get; set; }
        
        [JsonPropertyName("name")]
        public string ElementAliasName { get; set; }
        public bool IsExisted { get; set; }
    }
}
