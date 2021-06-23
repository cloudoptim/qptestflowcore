using System.Text.Json.Serialization;

namespace QPCore.Model.CompositeWebElements
{
    public class ChildCompositeWebElementResponse : EditChildCompositeWebElementRequest
    {
        [JsonPropertyName("pageId")]
        public int GroupId { get; set; }

        [JsonPropertyName("elementAliasName")]
        public string Name { get; set; }
    }
}