using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class TestFlowCategoryAssoc
    {
        public int TestFlowCatAssocId { get; set; }
        public int CategoryId { get; set; }
        public int TestFlowId { get; set; }

        public virtual TestFlowCategory Category { get; set; }
        public virtual TestFlow TestFlow { get; set; }
    }
}
