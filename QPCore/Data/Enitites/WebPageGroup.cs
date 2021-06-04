using System;
using System.Collections.Generic;
using QPCore.Data.BaseEntites;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebPageGroup : BaseEntity, IBaseEntity
    {
        public WebPageGroup()
        {
            this.WebPages = new HashSet<WebPage>();
        }
        public int? VersionId { get; set; }

        public virtual ICollection<WebPage> WebPages { get; set; }
    }
}
