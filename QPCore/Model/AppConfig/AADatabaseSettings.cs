using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationAssistant.Models.AppConfig
{

    public class AADatabaseSettings : IAADatabaseSettings
    {
     
        public string ConnectionString { get; set; }
       
    }

    public interface IAADatabaseSettings
    {
      
        string ConnectionString { get; set; }
    
    }

}
