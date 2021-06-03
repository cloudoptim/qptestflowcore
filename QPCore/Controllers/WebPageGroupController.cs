using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
using QPCore.Model.WebPageGroups;
using QPCore.Service.Interfaces;

namespace QPCore.Controllers
{
    [Authorize]
    [Route("api/pagegroups")]
    public class WebPageGroupController : BaseController
    {
        private readonly IWebPageGroupService _service;
        public WebPageGroupController(IWebPageGroupService service)
        {
            _service = service;
        }

        /// <summary>
        /// Check existed page group id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("existed/{id:int:min(1)}")]
        public ActionResult<ExistedResponse> CheckExistedId(int id)
        {
            var result = _service.CheckExistedId(id);

            return Ok(result);
        }

        /// <summary>
        /// Check existed page group name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("existed")]
        public ActionResult<ExistedResponse> CheckExisted(WebPageGroupExistedNameRequest request)
        {
            var result = _service.CheckExistedName(request.Name, request.Id);

            return Ok(result);
        }

        /// <summary>
        /// Get all page groups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<PageGroupItemResponse>> Get()
        {
            var result = _service.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Get by page group Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int:min(1)}")]
        public ActionResult<PageGroupItemResponse> Get(int id)
        {
            var result = _service.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// Create new page group
        /// </summary>
        /// <param name="pageGroup"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PageGroupItemResponse>> Create(CreatePageGroupRequest pageGroup)
        {
            var isExistedName = _service.CheckExistedName(pageGroup.Name);
            if (isExistedName.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, pageGroup.Name)
                });
            }

            var result = await _service.CreateAsync(pageGroup, Account.UserId);

            return Ok(result);
        }

        /// <summary>
        /// Edit page group
        /// </summary>
        /// <param name="pageGroup"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<PageGroupItemResponse>> Edit(EditPageGroupRequest pageGroup)
        {
            var isExistedId = _service.CheckExistedId(pageGroup.Id);
            if (!isExistedId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, pageGroup.Id)
                });
            }

            var isExistedName = _service.CheckExistedName(pageGroup.Name, pageGroup.Id);
            if (isExistedName.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, pageGroup.Name)
                });
            }

            var result = await _service.EditAsync(pageGroup, Account.UserId);

            return Ok(result);
        }

        /// <summary>
        /// Delete page group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}