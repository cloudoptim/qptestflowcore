﻿using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebElement
    {
        public WebElement()
        {
            this.CompositeWebElements = new HashSet<CompositeWebElement>();
        }
        public int Elementid { get; set; }
        public int Pageid { get; set; }
        public string Elementaliasname { get; set; }
        public string Elementtype { get; set; }
        public string Itype { get; set; }
        public string IValue { get; set; }
        public string Framenavigation { get; set; }
        public string Command { get; set; }
        public string Locationpath { get; set; }
        public string Screenshot { get; set; }
        public int? Elementparentid { get; set; }
        public string Applicationsection { get; set; }
        public string Value { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual WebPage WebPage { get; set; }

        public virtual ICollection<CompositeWebElement> CompositeWebElements { get; set; }
    }
}
