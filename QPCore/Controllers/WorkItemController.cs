using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.WorkItemTypes;
using QPCore.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using QPCore.Model.Common;
using Microsoft.AspNetCore.Authorization;
using QPCore.Model.WorkItems;

namespace QPCore.Controllers
{
    [Authorize]
    public class WorkItemController : BaseController
    {
        private readonly IWorkItemTypeService _workItemTypeService;
        private readonly IWorkItemService _workItemService;

        public WorkItemController(IWorkItemTypeService workItemTypeService,
            IWorkItemService workItemService)
        {
            _workItemTypeService = workItemTypeService;
            _workItemService = workItemService;
        }

        [HttpPost]
        public async Task<ActionResult<WorkItemResponse>> Create(CreateWorkItemRequest request)
        {
            var isExistedName = _workItemService.CheckExistedName(request.Name);
            if (isExistedName.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, request.Name)
                });
            }

            var isExistedTypeId = _workItemTypeService.CheckExistedId(request.WorkItemTypeId);
            if (!isExistedTypeId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format("Work Item Type: " + CommonMessageList.NOT_FOUND_THE_ID, request.WorkItemTypeId)
                });
            }

            var result = await _workItemService.CreateAsync(request, Account.UserId);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<WorkItemResponse>> Edit(EditWorkItemRequest request)
        {
            var isExistedId = _workItemService.CheckExistedId(request.Id);
            if (!isExistedId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, request.Id)
                });
            }

            var isExistedTypeId = _workItemTypeService.CheckExistedId(request.WorkItemTypeId);
            if (!isExistedTypeId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format("Work Item Type: " + CommonMessageList.NOT_FOUND_THE_ID, request.WorkItemTypeId)
                });
            }

            var isExistedName = _workItemService.CheckExistedName(request.Name, request.Id);
            if (isExistedName.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, request.Name)
                });
            }

            var result = await _workItemService.EditAsync(request, Account.UserId);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<WorkItemResponse>> Get()
        {
            var result = _workItemService.GetAll();
            result = result.OrderByDescending(p => p.Id).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("azurefeature/{id:int:min(1)}")]
        public ActionResult<List<WorkItemResponse>> GetByAuzreFeatureId(int id)
        {
            var result = _workItemService.GetByAzureFeatureId(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("azureworkitem/{id:int:min(1)}")]
        public ActionResult<List<WorkItemResponse>> GetByAuzreWorkItemId(int id)
        {
            var result = _workItemService.GetByAzureWorkItemId(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("workitemtype/{id:int:min(1)}")]
        public ActionResult<List<WorkItemResponse>> GetByWorkItemTypeId(int id)
        {
            var result = _workItemService.GetByWorkItemTypeId(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _workItemService.DeleteAsync(id);

            return NoContent();
        }
    }
}