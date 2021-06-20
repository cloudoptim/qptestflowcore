using Microsoft.AspNetCore.Authorization;
using QPCore.Service.Interfaces;
using QPCore.Model.CompositeWebElements;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
using System.Linq;

namespace QPCore.Controllers
{
    [Authorize]
    [Route("api/compositewebelements")]
    public class CompositeWebElementsController : BaseController
    {
        private readonly ICompositeWebElementService _compositeService;
        private readonly IWebPageService _webPageService;
        
        public CompositeWebElementsController(ICompositeWebElementService compositeService,
            IWebPageService webPageService)
        {
            this._compositeService = compositeService;
            this._webPageService = webPageService;
        }

        /// <summary>
        /// Create new composite web element
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CompositeWebElementResponse>> CreateCompositeWebElement(CreateCompositeWebElementRequest model)
        {
            model.Childs = model.Childs.OrderBy(p => p.Index).Distinct().ToList();
            var isExistedPage = _webPageService.CheckExistedId(model.GroupId);
            if(!isExistedPage.IsExisted) 
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format("PageId: " + CommonMessageList.NOT_FOUND_THE_ID, model.GroupId)
                });
            }

            var isExistedName = _compositeService.CheckExistedName(model.Name, model.GroupId);
            if (isExistedName.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_NAME_STRING, model.Name)
                });
            }

            var response = await _compositeService.CreateAsync(model, Account.UserId);
        
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        public ActionResult<CompositeWebElementResponse> Get(int id)
        {
            var result = _compositeService.GetById(id);
            return Ok(result);
        }
        
        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _compositeService.DeleteAsync(id);
            return Ok();
        }
        
    }
}