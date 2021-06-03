using System;
using System.Collections.Generic;
using QPCore.Data.BaseEntites;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebPageGroup : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? VersionId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set;}
        public DateTime? UpdatedDate { get; set; }
    }
}
