using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebElement
    {
        public int Elementid { get; set; }
        public int Pageid { get; set; }
        public string Elementaliasname { get; set; }
        public string Elementtype { get; set; }
        public string Itype { get; set; }
        public string Ivalue { get; set; }
        public string Framenavigation { get; set; }
        public string Command { get; set; }
        public string Locationpath { get; set; }
        public string Screenshot { get; set; }
        public int? Elementparentid { get; set; }
        public string Applicationsection { get; set; }
        public string Value { get; set; }
    }
}
