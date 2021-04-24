using System;
using System.Collections.Generic;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class WebModelProp
    {
        public int PropId { get; set; }
        public int GroupId { get; set; }
        public string PropName { get; set; }
        public string PropType { get; set; }
        public int? WebElementId { get; set; }
        public string ValidationExpression { get; set; }
        public int? PropLength { get; set; }

        public virtual WebModelGroup Group { get; set; }
    }
}
