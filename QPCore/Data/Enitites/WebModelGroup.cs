using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebModelGroup
    {
        public WebModelGroup()
        {
            WebModelProps = new HashSet<WebModelProp>();
        }

        public int GroupId { get; set; }
        public int ModelId { get; set; }
        public string GroupName { get; set; }

        public virtual WebModel Model { get; set; }
        public virtual ICollection<WebModelProp> WebModelProps { get; set; }
    }
}
