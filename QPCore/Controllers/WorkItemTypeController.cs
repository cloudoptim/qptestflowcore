using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.WorkItemTypes;
using QPCore.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using QPCore.Model.Common;
using Microsoft.AspNetCore.Authorization;

namespace QPCore.Controllers
{
    [Authorize]
    public class WorkItemTypeController : BaseController
    {
        private readonly IWorkItemTypeService _workItemTypeService;

        public WorkItemTypeController(IWorkItemTypeService workItemTypeService)
        {
            _workItemTypeService = workItemTypeService;
        }

        [HttpPost]
        public async Task<ActionResult<WorkItemTypeResponse>> Create(CreateWorkItemTypeRequest request)
        {
            var isExistedName = _workItemTypeService.CheckExistedName(request.Name);
            if (isExistedName.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, request.Name)
                });
            }

            var result = await _workItemTypeService.CreateAsync(request, Account.UserId);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<WorkItemTypeResponse>> Edit(EditWorkItemTypeRequest request)
        {
            var isExistedId = _workItemTypeService.CheckExistedId(request.Id);
            if (!isExistedId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, request.Id)
                });
            }

            var isExistedName = _workItemTypeService.CheckExistedName(request.Name, request.Id);
            if (isExistedName.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, request.Name)
                });
            }

            var result = await _workItemTypeService.EditAsync(request, Account.UserId);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<WorkItemTypeResponse>> Get()
        {
            var result = _workItemTypeService.GetAll();
            result = result.OrderBy(p => p.Name).ToList();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _workItemTypeService.DeleteAsync(id);

            return NoContent();
        }
    }
}