using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.DataBaseModel.Commands;
using QPCore.Model.DataBaseModel.Configurations;
using QPCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AnyOrignPolicy")]
    [ApiController]
    public class TestConfigController : ControllerBase
    {
        private ConfigService configService;
        public TestConfigController(ConfigService pcommandService)
        {
            configService = pcommandService;
        }
        // GET: api/<CommandController>
        [HttpGet]
        public List<TestConfigViewModel> Get()
        {
            return configService.getConfigs();
        }

        // GET api/<CommandController>/5
        [HttpGet("{id}")]
        public TestConfigViewModel Get(int id)
        {
            return configService.getConfig(id);
        }
        // GET api/<CommandController>/5
        [HttpGet()]
        [Route("Key/{id}")]
        public TestConfigKeys GetKey(int id)
        {
            return configService.getConfigKey(id);
        }
        // POST api/<CommandController>
        [HttpPost]
        public TestConfig Post(TestConfig value)
        {
           return configService.CreateConfig(value);
        }

        [HttpPost]
        [Route("Key")]
        public TestConfigKeys PostKeys(TestConfigKeys value)
        {
            return configService.CreateConfigKey(value);
        }
        // PUT api/<CommandController>/5
        [HttpPut("{id}")]
        public TestConfig Put(int id, TestConfig value)
        {
            return configService.UpdateConfig(id,value);
        }
        [HttpPut()]
        [Route("Key/{id}")]
        public TestConfigKeys PutKeys(int id, TestConfigKeys value)
        {
            return configService.UpdateConfigKeys(id, value);
        }
        // DELETE api/<CommandController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            configService.DeleteConfig(id);
        }

        [HttpDelete()]
        [Route("Key/{id}")]
        public void Deletekey(int id)
        {
            configService.DeleteConfigKey(id);
        }
    }
}
