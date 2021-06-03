using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
using QPCore.Model.WebPages;
using QPCore.Service.Interfaces;

namespace QPCore.Controllers
{
    [Authorize]
    [Route("api/webpages")]
    public class WebPageController : BaseController
    {
        private readonly IWebPageService _service;

        private readonly IWebPageGroupService _pageGroupService;

        public WebPageController(IWebPageService service,
            IWebPageGroupService pageGroupService)
        {
            _service = service;
            _pageGroupService = pageGroupService;
        }

        /// <summary>
        /// Check existed page id
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
        /// Check existed page name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("existed")]
        public ActionResult<ExistedResponse> CheckExisted(WebPageExistedNameRequest request)
        {
            var result = _service.CheckExistedName(request.Name, request.GroupId, request.Id);

            return Ok(result);
        }

        /// <summary>
        /// Get all pages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<WebPageItemResponse>> Get()
        {
            var result = _service.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Get by page Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int:min(1)}")]
        public ActionResult<WebPageItemResponse> Get(int id)
        {
            var result = _service.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// Create new page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<WebPageItemResponse>> Create(CreateWebPageRequest page)
        {
            var isExistedName = _service.CheckExistedName(page.Name, page.GroupId);
            if (isExistedName.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, page.Name)
                });
            }

            var isExistedGroupId = _pageGroupService.CheckExistedId(page.GroupId);
            if (!isExistedGroupId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                { 
                    Message = string.Format("Page Group Id: " + CommonMessageList.NOT_FOUND_THE_ID, page.GroupId)
                });
            }

            var result = await _service.CreateAsync(page, Account.UserId);

            return Ok(result);
        }

        /// <summary>
        /// Edit page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<WebPageItemResponse>> Edit(EditWebPageRequest page)
        {
            var isExistedId = _service.CheckExistedId(page.Id);
            if (!isExistedId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, page.Id)
                });
            }

            var isExistedName = _service.CheckExistedName(page.Name, page.GroupId, page.Id);
            if (isExistedName.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, page.Name)
                });
            }

            var isExistedGroupId = _pageGroupService.CheckExistedId(page.GroupId);
            if (!isExistedGroupId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                { 
                    Message = string.Format("Page Group Id: " + CommonMessageList.NOT_FOUND_THE_ID, page.GroupId)
                });
            }

            var result = await _service.EditAsync(page, Account.UserId);

            return Ok(result);
        }

        /// <summary>
        /// Delete page
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