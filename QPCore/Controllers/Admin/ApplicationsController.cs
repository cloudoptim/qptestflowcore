using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Applications;
using QPCore.Model.Common;
using QPCore.Model.Organizations;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Controllers.Admin
{
    public class ApplicationsController : BaseAdminController
    {
        private readonly IApplicationService _applicationService;
        private readonly IOrganizationService _organizationService;

        public ApplicationsController(IApplicationService applicationService,
            IOrganizationService organizationService)
        {
            _applicationService = applicationService;
            _organizationService = organizationService;
        }

        /// <summary>
        /// Get All Applications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<ApplicationResponse>> GetAll()
        {
            var apps = _applicationService.GetAll();

            return Ok(apps);
        }

        /// <summary>
        /// Get application by Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("{clientId}")]
        public ActionResult<ApplicationResponse> GetById(int clientId)
        {
            var application = _applicationService.GetById(clientId);

            return Ok(application);
        }

        /// <summary>
        /// Check unique application name
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        [HttpGet("checkunique")]
        public ActionResult<CheckUniqueResponse> CheckUniqueApplication(string applicationName)
        {
            if (string.IsNullOrEmpty(applicationName))
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = ApplicationMessageList.REQUIRED_APPLICATION_NAME
                });
            }

            var isUniqueResult = _applicationService.CheckUniqueApplicationName(applicationName);

            return Ok(isUniqueResult);
        }

        /// <summary>
        /// Create new application
        /// </summary>
        /// <param name="createApplicationRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApplicationResponse>> Create(CreateApplicationRequest createApplicationRequest)
        {
            var isExistedName = _applicationService.CheckUniqueApplicationName(createApplicationRequest.ApplicationName);

            if (!isExistedName.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = ApplicationMessageList.EXISTED_APPLICATION_STRING
                }); 
            }

            var isExistedOrgId = _organizationService.CheckExistingId(createApplicationRequest.OrgId);
            if (!isExistedOrgId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = OrganizationMessageList.NOT_FOUND_ORGANIZATION_ID
                });
            }

            var application = await _applicationService.CreateAsync(createApplicationRequest, Account.UserId);

            return Ok(application);
        }

        /// <summary>
        /// Update application
        /// </summary>
        /// <param name="editApplicationRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ApplicationResponse>> Update(EditApplicationRequest editApplicationRequest)
        {
            var isExistedId = _applicationService.CheckExistingId(editApplicationRequest.ClientId);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = ApplicationMessageList.NOT_FOUND_APPLICATION_ID
                });
            }

            var isUnique = _applicationService.CheckUniqueApplicationName(editApplicationRequest.ApplicationName, editApplicationRequest.ClientId);
            if (!isUnique.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = ApplicationMessageList.EXISTED_APPLICATION_STRING
                });
            }

            var isExistedOrgId = _organizationService.CheckExistingId(editApplicationRequest.OrgId);
            if (!isExistedOrgId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = OrganizationMessageList.NOT_FOUND_ORGANIZATION_ID
                });
            }

            var updatedApplication = await _applicationService.UpdateAsync(editApplicationRequest, Account.UserId);
            if (updatedApplication == null)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = CommonMessageList.COMMON_ERROR_MESSAGE
                });
            }

            return Ok(updatedApplication);
        }

        /// <summary>
        /// Delete application by client id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpDelete("{clientId}")]
        public async Task<ActionResult> Delete(int clientId)
        {
            var isExistedId = _applicationService.CheckExistingId(clientId);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = ApplicationMessageList.NOT_FOUND_APPLICATION_ID
                });
            }

            var canDelete = _applicationService.CheckCanDeleting(clientId);
            if (!canDelete)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = ApplicationMessageList.CAN_NOT_DELETE_APPLICATION
                });
            }

            await _applicationService.DeleteAsync(clientId);

            return Ok();
        }
    }
}
