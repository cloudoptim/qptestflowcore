using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
using QPCore.Model.TestPlans;
using QPCore.Model.TestPlanTestCases;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Controllers
{
    [Authorize]
    public class TestPlanTestCaseController : BaseController
    {
        private readonly ITestPlanTestCaseService _testPlanTestCaseService;
        private readonly ITestPlanService _testPlanService;

        public TestPlanTestCaseController(ITestPlanTestCaseService testPlanTestCaseService,
            ITestPlanService testPlanService)
        {
            _testPlanTestCaseService = testPlanTestCaseService;
            _testPlanService = testPlanService;
        }

        /// <summary>
        /// Get all assignment by test plan id
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns></returns>
        [HttpGet("{testPlanId}")]
        public ActionResult<List<TestPlanTestCaseResponse>> GetByTestPlanId(int testPlanId)
        {
            var items = _testPlanTestCaseService.GetAll(testPlanId);

            return Ok(items);
        }

        /// <summary>
        /// Create new test plan assignment
        /// </summary>
        /// <param name="createTestPlanTestCaseRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<List<TestPlanTestCaseResponse>>> Create(CreateTestPlanTestCaseRequest createTestPlanTestCaseRequest)
        {
            var isExistedTestPlanId = _testPlanService.CheckExistedId(createTestPlanTestCaseRequest.TestPlanId);
            if (!isExistedTestPlanId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestPlanMessageList.NOT_FOUND_TEST_PLAN_ID
                });
            }

            var result = await _testPlanTestCaseService.CreateAsync(createTestPlanTestCaseRequest, Account.UserId);
            return Ok(result);
        }

        /// <summary>
        /// Update single test plan assignment
        /// </summary>
        /// <param name="editTestPlanTestCaseRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<TestPlanTestCaseResponse>> Update(EditTestPlanTestCaseRequest editTestPlanTestCaseRequest)
        {
            var isExistedId = _testPlanTestCaseService.CheckExistedId(editTestPlanTestCaseRequest.Id);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestPlanTestCaseMessageList.NOT_FOUND_TEST_PLAN_ASSIGNMENT_ID
                });
            }

            var updatedItem = await _testPlanTestCaseService.UpdateAsync̣̣(editTestPlanTestCaseRequest, Account.UserId);
            if (updatedItem == null)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = CommonMessageList.COMMON_ERROR_MESSAGE
                });
            }

            return Ok(updatedItem);
        }

        /// <summary>
        /// Delete test plan assignment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isExistedId = _testPlanTestCaseService.CheckExistedId(id);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestPlanTestCaseMessageList.NOT_FOUND_TEST_PLAN_ASSIGNMENT_ID
                });
            }

            await _testPlanTestCaseService.DeleteAsync(id);

            return Ok();
        }
    }
}
