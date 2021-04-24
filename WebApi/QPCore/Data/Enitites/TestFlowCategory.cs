using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class TestFlowCategory
    {
        public TestFlowCategory()
        {
            TestFlowCategoryAssocs = new HashSet<TestFlowCategoryAssoc>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public BitArray IsActive { get; set; }
        public int ClientId { get; set; }

        public virtual Application Client { get; set; }
        public virtual ICollection<TestFlowCategoryAssoc> TestFlowCategoryAssocs { get; set; }
    }
}
