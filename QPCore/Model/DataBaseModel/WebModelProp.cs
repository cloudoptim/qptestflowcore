
namespace DataBaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class WebModelProp
    {
       
        public int PropId { get; set; }
        public int GroupId { get; set; }
        public string PropName { get; set; }
        public string PropType { get; set; }
        public int? PropLength { get; set; }
        public string ValidationExpression { get; set; }
        public Nullable<int> WebElementId { get; set; }
        public WebElement webElement { set; get; }
      

    }
}
