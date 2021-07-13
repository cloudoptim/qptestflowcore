using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
using QPCore.Model.TestFlowCategories;
using QPCore.Service.Interfaces;

namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestFlowCategoriesController : BaseController
    {
        private readonly ITestFlowCategoryService _testFlowCategoryService;
        private readonly IApplicationService _applicationService;

        public TestFlowCategoriesController(ITestFlowCategoryService testFlowCategoryService,
            IApplicationService applicationService)
        {
            _testFlowCategoryService = testFlowCategoryService;
            _applicationService = applicationService;
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TestFlowCategoryResponse>> Create(CreateTestFlowCategoryRequest model)
        {
            var isExisted = _applicationService.CheckExistingId(model.ClientId);
            if (!isExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format("Client Id: " + CommonMessageList.NOT_FOUND_THE_ID, model.ClientId)
                });
            }

            var isUnique = _testFlowCategoryService.CheckUnique(model.CategoryName);
            if (!isUnique.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, model.CategoryName)
                });
            };

            var dataItem = await _testFlowCategoryService.AddAsync(model, Account.UserId);
            return Ok(dataItem);
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<TestFlowCategoryResponse>> Update(EditTestFlowCategoryRequest model)
        {
            var isExisted = _testFlowCategoryService.CheckExistedId(model.CategoryId);
            if (!isExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, model.CategoryId)
                });
            }

            var isExistedClient = _applicationService.CheckExistingId(model.ClientId);
            if (!isExistedClient)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format("Client Id: " + CommonMessageList.NOT_FOUND_THE_ID, model.ClientId)
                });
            }

            var isUnique = _testFlowCategoryService.CheckUnique(model.CategoryName, model.CategoryId);
            if (!isUnique.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, model.CategoryName)
                });
            };

            var response = await _testFlowCategoryService.UpdateAsync(model, Account.UserId);
            if (response == null)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = CommonMessageList.COMMON_ERROR_MESSAGE
                });
            }

            return Ok(response);
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("{categoryId}")]
        public ActionResult<TestFlowCategoryResponse> GetById(int categoryId)
        {
            var isExisted = _testFlowCategoryService.CheckExistedId(categoryId);
            if (!isExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, categoryId)
                });
            }

            var dataItem = _testFlowCategoryService.GetById(categoryId);
            return Ok(dataItem);
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult<List<TestFlowCategoryResponse>> GetAll()
        {
            var dataItems = _testFlowCategoryService.GetAll();
            return Ok(dataItems);
        }

        /// <summary>
        /// Get all categories by client id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        /// 
        [HttpGet("client/{clientId}")]
        public ActionResult<List<TestFlowCategoryResponse>> GetByClientId(int clientId)
        {
            var dataItems = _testFlowCategoryService.GetByClientId(clientId);
            return Ok(dataItems);
        }

        /// <summary>
        /// Get category by type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyword"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public ActionResult<List<TestFlowCategoryResponse>> GetByType(string type, string keyword, int skip = 0, int limit = 20)
        {
            var dataItems = _testFlowCategoryService.GetByType(type, keyword, skip, limit);
            return Ok(dataItems);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> Delete(int categoryId)
        {
            var isExisted = _testFlowCategoryService.CheckExistedId(categoryId);
            if (!isExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, categoryId)
                });
            }

            var canDelete = _testFlowCategoryService.CheckCanDeleting(categoryId);
            if (!canDelete)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = CommonMessageList.CAN_NOT_DELETE_ITEM
                });
            }
            await _testFlowCategoryService.Delete(categoryId);
            return Ok();
        }
    }
}