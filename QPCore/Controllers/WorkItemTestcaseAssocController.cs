using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.WorkItemTestcaseAssoc;
using QPCore.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using QPCore.Model.Common;
using Microsoft.AspNetCore.Authorization;
using QPCore.Model.WorkItems;
using QPCore.Service;

namespace QPCore.Controllers
{
    [Authorize]
    public class WorkItemTestcaseController : BaseController
    {
        private readonly IWorkItemTestcaseAssocService _workItemTestcaseService;
        private readonly IWorkItemService _workItemService;
        private readonly TestFlowService _testflowService;

        public WorkItemTestcaseController(IWorkItemService workItemService,
            IWorkItemTestcaseAssocService workItemTestcaseService,
            TestFlowService testFlowService)
        {
            _workItemTestcaseService = workItemTestcaseService;
            _workItemService = workItemService;
            _testflowService = testFlowService;
        }

        [HttpPost]
        public async Task<ActionResult<WorkItemTestcaseAssocResponse>> Create(CreateWorkItemTestcaseAssocRequest request)
        {
            var isExistedWorkItemId = _workItemService.CheckExistedId(request.WorkItemId);
            if (!isExistedWorkItemId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format("Work Item: " + CommonMessageList.NOT_FOUND_THE_ID, request.WorkItemId)
                });
            }

            var isExistedTestflowId = _testflowService.CheckExistedId(request.TestcaseId);
            if (!isExistedTestflowId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format("Test case: " + CommonMessageList.NOT_FOUND_THE_ID, request.TestcaseId)
                });
            }

            var result = await _workItemTestcaseService.CreateAsync(request, Account.UserId);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<WorkItemTestcaseAssocResponse>> Edit(EditWorkItemTestcaseAssocRequest request)
        {
            var isExistedId = _workItemTestcaseService.CheckExistedId(request.Id);
            if (!isExistedId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, request.Id)
                });
            }

            var isExistedWorkItemId = _workItemService.CheckExistedId(request.WorkItemId);
            if (!isExistedWorkItemId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format("Work Item: " + CommonMessageList.NOT_FOUND_THE_ID, request.WorkItemId)
                });
            }

            var isExistedTestflowId = _testflowService.CheckExistedId(request.TestcaseId);
            if (!isExistedTestflowId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format("Test case: " + CommonMessageList.NOT_FOUND_THE_ID, request.TestcaseId)
                });
            }

            var result = await _workItemTestcaseService.EditAsync(request, Account.UserId);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<WorkItemTestcaseAssocResponse>> Get()
        {
            var result = _workItemTestcaseService.GetAll();
            result = result.OrderByDescending(p => p.Id).ToList();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _workItemTestcaseService.DeleteAsync(id);

            return NoContent();
        }
    }
}