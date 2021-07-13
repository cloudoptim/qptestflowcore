using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
using QPCore.Model.TestFlowCategoryAssocs;
using QPCore.Service;
using QPCore.Service.Interfaces;

namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestFlowCategoryAssocsController : BaseController
    {
        private readonly ITestFlowCategoryAssocService _testFlowCategoryAssocService;
        private readonly TestFlowService _testFlowService;
        private readonly ITestFlowCategoryService _testFlowCategoryService;
        public TestFlowCategoryAssocsController(ITestFlowCategoryAssocService testFlowCategoryAssocService,
            TestFlowService testFlowService,
            ITestFlowCategoryService testFlowCategoryService)
        {
            _testFlowCategoryAssocService = testFlowCategoryAssocService;
            _testFlowService = testFlowService;
            _testFlowCategoryService = testFlowCategoryService;
        }

        /// <summary>
        /// Create new associate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TestFlowCategoryAssocResponse>> Create(CreateTestFlowCategoryAssocRequest model)
        {
            var isCategoryExisted = _testFlowCategoryService.CheckExistedId(model.CategoryId);
            if (!isCategoryExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format("TestFlowCategory Id: " + CommonMessageList.NOT_FOUND_THE_ID, model.CategoryId)
                });
            }

            var isTestFlowExisted = _testFlowService.CheckExistedId(model.TestFlowId);
            if (!isTestFlowExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format("TestFlow Id: " + CommonMessageList.NOT_FOUND_THE_ID, model.TestFlowId)
                });
            }

            var isExistedAssociation = _testFlowCategoryAssocService.CheckExistedAssociation(model.TestFlowId, model.CategoryId);
            if (isExistedAssociation)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = TestFlowCategoryAssocMessageList.EXISTED_ASSOCIATION_STRING
                });
            }

            var dataResponse = await _testFlowCategoryAssocService.AddAsync(model);
            return Ok(dataResponse);
        }

        /// <summary>
        /// Multiple assign categories to testcases
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("bulk")]
        public async Task<ActionResult> Bulk(BulkCreateRequest request)
        {
            await _testFlowCategoryAssocService.BulkAsync(request);
            return Ok();
        }

        /// <summary>
        /// Delete association
        /// </summary>
        /// <param name="testFlowCategoryAssocId"></param>
        /// <returns></returns>
        [HttpDelete("{testFlowCategoryAssocId}")]
        public async Task<ActionResult> Delete(int testFlowCategoryAssocId)
        {
            var isExistedId = _testFlowCategoryAssocService.CheckExistedId(testFlowCategoryAssocId);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, testFlowCategoryAssocId)
                });
            }

            await _testFlowCategoryAssocService.DeleteAsync(testFlowCategoryAssocId);
            return Ok();
        }

        /// <summary>
        /// Get associations by testflow id
        /// </summary>
        /// <param name="testFlowId"></param>
        /// <returns></returns>
        /// 
        [HttpGet("testflow/{testFlowId}")]
        public ActionResult<List<TestFlowCategoryAssocResponse>> GetByTestFlowId(int testFlowId)
        {
            var dataItems = _testFlowCategoryAssocService.GetByTestFlowId(testFlowId);
            return Ok(dataItems);
        }

        /// <summary>
        /// Get associations by category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("category/{categoryId}")]
        public ActionResult<List<TestFlowCategoryAssocResponse>> GetByCategoryId(int categoryId)
        {
            var dataItems = _testFlowCategoryAssocService.GetByCategoryId(categoryId);
            return Ok(dataItems);
        }
    }
}