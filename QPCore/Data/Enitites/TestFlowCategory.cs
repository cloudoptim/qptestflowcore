﻿using System;
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
            TestFlows = new HashSet<TestFlow>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public int ClientId { get; set; }
        public string Type { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public virtual Application Client { get; set; }
        public virtual ICollection<TestFlowCategoryAssoc> TestFlowCategoryAssocs { get; set; }
        public virtual ICollection<TestFlow> TestFlows { get; set; }
    }
}
