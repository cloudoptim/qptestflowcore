using System;
using System.Collections.Generic;
using QPCore.Data.BaseEntites;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebPageGroup : BaseEntity, IBaseEntity
    {
        public int? VersionId { get; set; }
    }
}
