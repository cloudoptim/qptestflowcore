using System.Collections.Generic;

namespace QPCore.Data.Enitites
{
    public class CompositeWebElement : BaseEntity
    {
        public CompositeWebElement()
        {
            this.Childs = new HashSet<CompositeWebElement>();
        }
        public int PageId { get; set; }

        public bool IsComposite { get; set; }

        public int Index { get; set; }

        public string Command { get; set; }

        public int? ParentId { get; set; }

        public int? WebElementId { get; set; }

        public virtual WebPage WebPage { get; set; }

        public virtual CompositeWebElement Parent { get; set; }

        public virtual WebElement WebElement { get; set; }

        public virtual ICollection<CompositeWebElement> Childs { get; set; }

    }
}