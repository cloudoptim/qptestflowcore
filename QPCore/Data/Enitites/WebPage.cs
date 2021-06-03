using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebPage : BaseEntity
    {
        public int GroupId { get; set; }

        public virtual WebPageGroup WebPageGroup { get; set; }
    }
}
