using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Model.DataBaseModel.Configurations
{
    public class TestConfigViewModel
    {
        public TestConfigViewModel()
        {
            Keys = new List<TestConfigKeys>();
        }
        public int ConfigId { set; get; }
        public int ClientId { set; get; }
        public string ConfigName { set; get; }
        public bool IsActive { set; get; }
        public bool IsSystemDefined { set; get; }
        public List<TestConfigKeys> Keys { set; get; }
    }
}
