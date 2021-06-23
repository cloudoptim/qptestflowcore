using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;

namespace QPCore.Model.WebElement
{

    public class WebPageTreeItem
    {
        public WebPageTreeItem()
        {
            this.CompositeElements = new List<ElementTreeItem>();
            this.WebElements = new List<ElementTreeItem>();
        }
        public int PageId { get; set; }
        public int GroupId { get; set; }
        public string PageName { get; set; }

        [JsonIgnore]
        public IEnumerable<ElementTreeItem> CompositeElements { get; set; }

        [JsonIgnore]
        public IEnumerable<ElementTreeItem> WebElements { get; set; }

        public List<ElementTreeItem> Elements
        {
            get
            {
                var items = this.CompositeElements
                    .Union(this.WebElements)
                    .OrderBy(p => p.ElementName)
                    .ToList();
                return items;
            }
        }
    }

}