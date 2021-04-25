using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
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
    public class TestFlowController : BaseController
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

        [HttpGet("checkunique")]
        public CheckUniqueDTO CheckUniqueTestFlow(string name)
        {
            var result = testFlowService.CheckUniqueTestFlow(name);
            return result;
        }

        [HttpPost("lock")]
        [Authorize]
        public async Task<IActionResult> LockTestFlow(int id)
        {
            var userId = Account.UserId;
            if (userId < 0)
            {
                return BadRequest("You should add UserId into to header of request: Ex: request.Header['UserId']= 1");
            }

            var lockedResult = await testFlowService.LockTestFlowAsync(id, userId);
            if (lockedResult == null)
            {
                return BadRequest("The user id or test flow id is invalid.");
            }

            return Ok(lockedResult);
        }

        [HttpPost("unlock")]
        [Authorize]
        public async Task<IActionResult> UnlockTestFlow(int id)
        {
            var userId = Account.UserId;
            if (userId < 0)
            {
                return BadRequest("You should add UserId into to header of request: Ex: request.Header['UserId']= 1");
            }
            var lockedResult = await testFlowService.UnlockTestFlowAsync(id, userId);

            if (lockedResult == null)
            {
                return BadRequest("The user id or test flow id is invalid.");
            }
            return Ok(lockedResult);
        }

        [HttpGet("checklocking")]
        [Authorize]
        public ActionResult<CheckLockingDTO> CheckLockingTestFlow(int id)
        {
            var userId = Account.UserId;
            if (userId < 0)
            {
                return BadRequest("You should add UserId into to header of request: Ex: request.Header['UserId']= 1");
            }
            var result = testFlowService.CheckLockedTestFlow(id, userId);
            return Ok(result);
        }
    }
}
