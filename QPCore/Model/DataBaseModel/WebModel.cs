
namespace DataBaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class WebModel
    {
       
        public WebModel()
        {
            this.WebModelGroups = new List<WebModelGroup>();
        }
    
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string FeatureName { get; set; }
        public string ApplicationName { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
        public  IList<WebModelGroup> WebModelGroups { get; set; }
    }
}
