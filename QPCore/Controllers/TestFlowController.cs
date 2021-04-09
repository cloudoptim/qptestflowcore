using AutoMapper;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors("AnyOrignPolicy")]
    [ApiController]
    public class TestFlowController : ControllerBase
    {
        TestFlowService testFlowService;
        private readonly IMapper _mapper;

        public TestFlowController(TestFlowService testFlowService, IMapper mapper)
        {
            this.testFlowService = testFlowService;
            this._mapper = mapper;
        }
        // GET: api/<TestFlowController>
        [HttpGet]
        public List<TestFlow> Get()
        {
            return testFlowService.GetTestFlows();
        }

        // GET api/<TestFlowController>/5
        [HttpGet("{id}")]
        public TestFlowDTO Get(int id)
        {
            var testFlow = testFlowService.GetTestFlow(id);
            var result = this._mapper.Map<TestFlowDTO>(testFlow);
            return result;
        }

        // POST api/<TestFlowController>
        [HttpPost]
        public TestFlowDTO Post(TestFlowDTO value)
        {
            var testFlow = this._mapper.Map<TestFlow>(value);
            testFlow = testFlowService.CreateTestFlow(testFlow);

            var result = this._mapper.Map<TestFlowDTO>(testFlow);
            return result;
        }

        // PUT api/<TestFlowController>/5
        [HttpPut("{id}")]
        public TestFlowDTO Put(int id, TestFlowDTO value)
        {
            var testFlow = this._mapper.Map<TestFlow>(value);

            testFlow = testFlowService.UpdateTestFlow(id, testFlow);

            var result = this._mapper.Map<TestFlowDTO>(testFlow);
            return result;
        }

        // DELETE api/<TestFlowController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            testFlowService.DeleteTestFlow(id);
        }
    }
}
