namespace DataBaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class WebPageGroup
    {
        public WebPageGroup()
        {
            this.WebPages = new List<WebPage>();
        }
        public int id { get; set; }
        public string groupname { get; set; }
        public string createdby { get; set; }
        public Nullable<System.DateTime> createddatetime { get; set; }
        public string updatename { get; set; }
        public Nullable<System.DateTime> updateddatetime { get; set; }
        public Nullable<bool> isactive { get; set; }
        public Nullable<int> versionid { get; set; }

        public List<WebPage> WebPages { set; get; }
    }
}
