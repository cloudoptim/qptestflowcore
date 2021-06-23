using System.Collections.Generic;

namespace QPCore.Model.WebElement
{
    public class WebPageGroupTree
    {
        public WebPageGroupTree()
        {
            this.WebPages = new List<WebPageTreeItem>();
        }
        public int Id { get; set; }

        public string GroupName { get; set; }

        public IEnumerable<WebPageTreeItem> WebPages { get; set; }
    }
}