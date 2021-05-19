using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
using QPCore.Model.TestPlans;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Controllers
{
    [Authorize]
    public class TestPlanController : BaseController
    {
        private readonly ITestPlanService _testPlanService;
        private readonly IAccountService _accountService;
        public TestPlanController(ITestPlanService testPlanService,
            IAccountService accountService)
        {
            _testPlanService = testPlanService;
            _accountService = accountService;
        }

        /// <summary>
        /// Get all test plans
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<TestPlanResponse>> GetAll()
        {
            var testPlans = _testPlanService.GetAll();

            return Ok(testPlans);
        }

        /// <summary>
        /// Get test plan by Id
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns></returns>
        [HttpGet("{testPlanId}")]
        public ActionResult<TestPlanResponse> GetById(int testPlanId)
        {
            var isExistedId = _testPlanService.CheckExistedId(testPlanId);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestPlanMessageList.NOT_FOUND_TEST_PLAN_ID
                });
            }

            var testPlan = _testPlanService.GetById(testPlanId);

            return Ok(testPlan);
        }

        /// <summary>
        /// Get test plan by parent id
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet("parents/{parentId}")]
        public ActionResult<TestPlanResponse> GetByParentId(int parentId)
        {
            var isExistedId = _testPlanService.CheckExistedId(parentId);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestPlanMessageList.NOT_FOUND_PARENT_TEST_PLAN_ID
                });
            }

            var testPlans = _testPlanService.GetByParentId(parentId);

            return Ok(testPlans);
        }

        /// <summary>
        /// Check unique test plan name
        /// </summary>
        /// <param name="testPlanName"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet("checkunique")]
        public ActionResult<CheckUniqueResponse> CheckUniqueName(string testPlanName, int? parentId)
        {
            if (string.IsNullOrEmpty(testPlanName))
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestPlanMessageList.REQUIRED_TEST_PLAN_NAME
                });
            }

            var isUniqueResult = _testPlanService.CheckUniqueName(testPlanName, parentId);

            return Ok(isUniqueResult);
        }


        /// <summary>
        /// Create new test plan
        /// </summary>
        /// <param name="createTestPlanRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TestPlanResponse>> Create(CreateTestPlanRequest createTestPlanRequest)
        {
            var isExistedName = _testPlanService.CheckUniqueName(createTestPlanRequest.Name, createTestPlanRequest.ParentId);
            if (!isExistedName.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(TestPlanMessageList.EXISTED_TEST_PLAN_STRING, createTestPlanRequest.Name)
                });
            }

            if (createTestPlanRequest.ParentId.HasValue)
            {
                var isExistedParent = _testPlanService.CheckExistedId(createTestPlanRequest.ParentId.Value);
                if (!isExistedParent)
                {
                    return BadRequest(new BadRequestResponse()
                    {
                        Message = TestPlanMessageList.NOT_FOUND_PARENT_TEST_PLAN_ID
                    });
                }
            }

            var isExistedAssignTo = _accountService.CheckExistedId(createTestPlanRequest.AssignTo);
            if (!isExistedAssignTo)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestPlanMessageList.NOT_FOUND_ASSIGN_TO_ID
                });
            }

            var testPlan = await _testPlanService.CreateAsync(createTestPlanRequest, Account.UserId);
            return Ok(testPlan);
        }

        /// <summary>
        /// Update test plan
        /// </summary>
        /// <param name="editTestPlanRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<TestPlanResponse>> Update(EditTestPlanRequest editTestPlanRequest)
        {
            var isExistedName = _testPlanService.CheckUniqueName(editTestPlanRequest.Name, editTestPlanRequest.ParentId, editTestPlanRequest.Id);
            if (!isExistedName.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(TestPlanMessageList.EXISTED_TEST_PLAN_STRING, editTestPlanRequest.Name)
                });
            }

            if (editTestPlanRequest.ParentId.HasValue)
            {
                var isExistedParent = _testPlanService.CheckExistedId(editTestPlanRequest.ParentId.Value);
                if (!isExistedParent)
                {
                    return BadRequest(new BadRequestResponse()
                    {
                        Message = TestPlanMessageList.NOT_FOUND_PARENT_TEST_PLAN_ID
                    });
                }
            }


            var isExistedId= _testPlanService.CheckExistedId(editTestPlanRequest.Id);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestPlanMessageList.NOT_FOUND_TEST_PLAN_ID
                });
            }


            var isExistedAssignTo = _accountService.CheckExistedId(editTestPlanRequest.AssignTo);
            if (!isExistedAssignTo)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestPlanMessageList.NOT_FOUND_ASSIGN_TO_ID
                });
            }

            var updatedTestPlan = await _testPlanService.UpdateAsync(editTestPlanRequest, Account.UserId);

            if (updatedTestPlan == null)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = CommonMessageList.COMMON_ERROR_MESSAGE
                });
            }

            return Ok(updatedTestPlan);
        }

        /// <summary>
        /// Delete test plan by id
        /// </summary>
        /// <param name="testPlanId"></param>
        /// <returns></returns>
        [HttpDelete("{testPlanId}")]
        public async Task<ActionResult> Delete(int testPlanId)
        {
            var isExistedId = _testPlanService.CheckExistedId(testPlanId);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestPlanMessageList.NOT_FOUND_TEST_PLAN_ID
                });
            }

            var canDelete = _testPlanService.CheckCanDeleting(testPlanId);
            if (!canDelete)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestPlanMessageList.CAN_NOT_DELETE_TEST_PLAN
                });
            }

            await _testPlanService.DeleteAsync(testPlanId);

            return Ok();
        }
    }
}
