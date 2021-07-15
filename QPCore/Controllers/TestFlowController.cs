using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
using QPCore.Model.DataBaseModel.TestFlows;
using QPCore.Model.TestFlows;
using QPCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
        public List<TestFlowItemResponse> Get(string keyword = "", int clientId = 1)
        {
            return testFlowService.GetTestFlowItems(keyword, clientId);
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
        public ActionResult<TestFlowDTO> Post(TestFlowDTO value)
        {

            var isUnique = testFlowService.CheckUniqueTestFlow(value.TestFlowName);
            if (!isUnique.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(TestFlowMessageList.EXISTED_NAME_STRING, value.TestFlowName)
                });
            }

            var testFlow = this._mapper.Map<TestFlow>(value);
            testFlow = testFlowService.CreateTestFlow(testFlow, Account.UserId);

            var result = this._mapper.Map<TestFlowDTO>(testFlow);
            return result;
        }

        // PUT api/<TestFlowController>/5
        [HttpPut("{id}")]
        public ActionResult<TestFlowDTO> Put(int id, TestFlowDTO value)
        {
            var isExistId = testFlowService.CheckExistedId(id);
            if (!isExistId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestFlowMessageList.NOT_FOUND_ID
                });
            }

            var isUnique = testFlowService.CheckUniqueTestFlow(value.TestFlowName, id);
            if (!isUnique.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(TestFlowMessageList.EXISTED_NAME_STRING, value.TestFlowName)
                });
            }

            var testFlow = this._mapper.Map<TestFlow>(value);

            testFlow = testFlowService.UpdateTestFlow(id, testFlow, Account.UserId);

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
        public CheckUniqueResponse CheckUniqueTestFlow(string name)
        {
            var result = testFlowService.CheckUniqueTestFlow(name);
            return result;
        }

        [HttpPost("lock")]
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
