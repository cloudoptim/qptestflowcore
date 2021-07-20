using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
using QPCore.Model.Integrations;
using QPCore.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace QPCore.Controllers
{
    [Authorize]
    public class IntegrationsController : BaseController
    {
        private readonly IIntegrationService _integrationService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public IntegrationsController(IIntegrationService integrationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _integrationService = integrationService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public ActionResult<IntegrationResponse> GetSources()
        {
            var result = _integrationService.GetAllSources(this.Account.UserId);
            Parallel.ForEach(result, item =>
            {
                item.BindObsoluteLogoPath(_httpContextAccessor);
            });

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IntegrationResponse> Get(int id)
        {
            var existedId = _integrationService.CheckExistedId(id);
            if (!existedId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, id)
                });
            }

            var result = _integrationService.GetById(id);
            result.BindObsoluteLogoPath(_httpContextAccessor);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IntegrationResponse>> Create(CreateIntegrationRequest model)
        {
            model.UserId = this.Account.UserId;
            model.IsActive = true;

            var isExisted = _integrationService.CheckExistedAssignment(model.SourceId,this.Account.UserId);
            if (isExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.EXISTED_ASSIGNMENT_STRING, model.SourceId, model.UserId)
                });
            }
            
            var result = await _integrationService.CreateAsync(model, Account.UserId);
            result.BindObsoluteLogoPath(_httpContextAccessor);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<IntegrationResponse>> Update(EditIntegrationRequest model)
        {
            var existedId = _integrationService.CheckExistedId(model.Id);
            if (!existedId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, model.Id)
                });
            }

            var result = await _integrationService.EditAsync(model, Account.UserId);
            result.BindObsoluteLogoPath(_httpContextAccessor);

            return Ok(result);
        }

        [HttpPut("pat")]
        public async Task<ActionResult<IntegrationResponse>> UpdatePat(EditPatRequest model)
        {
            var existedId = _integrationService.CheckExistedId(model.Id);
            if (!existedId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, model.Id)
                });
            }

            var result = await _integrationService.EditPatAsync(model, Account.UserId);
            result.BindObsoluteLogoPath(_httpContextAccessor);

            return Ok(result);
        }

        [HttpPut("active")]
        public async Task<ActionResult<IntegrationResponse>> UpdatePat(EditActivateRequest model)
        {
            var existedId = _integrationService.CheckExistedId(model.Id);
            if (!existedId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, model.Id)
                });
            }

            var result = await _integrationService.EditActivationAsync(model, Account.UserId);
            result.BindObsoluteLogoPath(_httpContextAccessor);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existedId = _integrationService.CheckExistedId(id);
            if (!existedId.IsExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = string.Format(CommonMessageList.NOT_FOUND_THE_ID, id)
                });
            }

            await _integrationService.DeleteAsync(id);
            return Ok();
        }
    }
}