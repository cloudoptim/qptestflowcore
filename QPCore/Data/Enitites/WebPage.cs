using System.Collections.Generic;

namespace QPCore.Data.Enitites
{
    public partial class WebPage : BaseGroupEntity
    {
        public WebPage()
        {
            this.CompositeWebElements = new HashSet<CompositeWebElement>();
            this.WebElements = new HashSet<WebElement>();
        }
        
        public bool IsActive { get; set; }
        public virtual WebPageGroup WebPageGroup { get; set; }

        public virtual ICollection<WebElement> WebElements { get; set; }
        public virtual ICollection<CompositeWebElement> CompositeWebElements { get; set; }
    }
}
