using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace QPCore.Model.CompositeWebElements
{
    public class CompositeWebElementResponse
    {     
        public CompositeWebElementResponse()
        {
            this.Childs = new List<ChildCompositeWebElementResponse>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("pageId")]
        public int GroupId { get; set; }

        public string Command { get; set; }

        public int? ParentId { get; set; }

        public List<ChildCompositeWebElementResponse> Childs {get ; set;}
    }
}