using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebPageGroup
    {
        public int Id { get; set; }
        public string Groupname { get; set; }
        public string Createdby { get; set; }
        public DateTime? Createddatetime { get; set; }
        public string Updatedby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public int? Versionid { get; set; }
    }
}
