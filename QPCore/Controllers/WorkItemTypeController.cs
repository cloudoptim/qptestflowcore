using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.WorkItemTypes;
using QPCore.Service.Interfaces;
using System.Linq;

namespace QPCore.Controllers
{
    public class WorkItemTypeController : BaseController
    {
        private readonly IWorkItemTypeService _workItemTypeService;

        public WorkItemTypeController(IWorkItemTypeService workItemTypeService)
        {
            _workItemTypeService = workItemTypeService;
        }

        [HttpGet]
        public ActionResult<List<WorkItemTypeResponse>> Get()
        {
            var result = _workItemTypeService.GetAll();
            result = result.OrderBy(p => p.Name).ToList();
            return Ok(result);
        }
        
    }
}