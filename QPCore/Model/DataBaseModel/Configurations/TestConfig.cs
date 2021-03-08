using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.DataBaseModel.Configurations
{
    public class TestConfig
    {
        public TestConfig()
        {
         
        }
        public int ConfigId { set; get; }
        public int ClientId { set; get; }
        public string ConfigName { set; get; }
        public bool IsActive { set; get; }
        public bool IsSystemDefined { set; get; }
       
    }
}
