using Microsoft.AspNetCore.Mvc;
using QPCore.Model.DataBaseModel.TestFlows;
using QPCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestFlowController : ControllerBase
    {
        TestFlowService testFlowService;
        public TestFlowController(TestFlowService testFlowService)
        {
            this.testFlowService = testFlowService;
        }
        // GET: api/<TestFlowController>
        [HttpGet]
        public List<TestFlow> Get()
        {
            return testFlowService.GetTestFlows();
        }

        // GET api/<TestFlowController>/5
        [HttpGet("{id}")]
        public TestFlow Get(int id)
        {
            return testFlowService.GetTestFlow(id);
        }

        // POST api/<TestFlowController>
        [HttpPost]
        public TestFlow Post(TestFlow value)
        {
            return testFlowService.CreateTestFlow(value);
        }

        // PUT api/<TestFlowController>/5
        [HttpPut("{id}")]
        public TestFlow Put(int id, TestFlow value)
        {
            return testFlowService.UpdateTestFlow(id,value);
        }

        // DELETE api/<TestFlowController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            testFlowService.DeleteTestFlow(id);
        }
    }
}
