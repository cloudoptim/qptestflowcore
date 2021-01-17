
namespace DataBaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class WebPage
    {
        public int pageid { get; set; }
        public int groupid { get; set; }
        public string pagename { get; set; }
        public string createdby { get; set; }
        public Nullable<System.DateTime> createddatetime { get; set; }
        public string updatename { get; set; }
        public Nullable<System.DateTime> updateddatetime { get; set; }
        public Nullable<bool> isactive { get; set; }
    }
}
