using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebPage
    {
        public int Pageid { get; set; }
        public int Groupid { get; set; }
        public string Pagename { get; set; }
        public string Createdby { get; set; }
        public DateTime? Createddatetime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public BitArray Isactive { get; set; }
    }
}
