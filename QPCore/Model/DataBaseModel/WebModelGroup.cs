
namespace DataBaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class WebModelGroup
    {
        public WebModelGroup()
        {
            this.WebModelProps = new List<WebModelProp>();
        }
        public int GroupId { get; set; }
        public int ModelId { get; set; }
        public string GroupName { get; set; }
        public int AssocId { get; set; }
        public  IList<WebModelProp> WebModelProps { get; set; }

    }
}
