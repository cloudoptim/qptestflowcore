using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Common;
using QPCore.Model.Organizations;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Controllers.Admin
{
    public class OrganizationsController : BaseAdminController
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationsController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        /// Get all organizations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<OrganizationResponse>> GetAll()
        {
            var orgs = _organizationService.GetAll();

            return Ok(orgs);
        }

        /// <summary>
        /// Get organization by OrgId
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpGet("{orgId}")]
        public ActionResult<OrganizationResponse> GetById(int orgId)
        {
            var org = _organizationService.GetById(orgId);

            return Ok(org);
        }

        /// <summary>
        /// Create new Organization
        /// </summary>
        /// <param name="createOrganization"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<OrganizationResponse>> Create(CreateOrganizationRequest createOrganization)
        {
            var isExistedName = _organizationService.CheckUniqueOrgName(createOrganization.OrgName);

            if (!isExistedName.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = OrganizationMessageList.EXISTED_ORGANIZATION_STRING
                });
            }
            var org = await _organizationService.AddAsync(createOrganization, Account.UserId);

            return Ok(org);
        }

        /// <summary>
        /// Check unique organization name
        /// </summary>
        /// <param name="organizationName"></param>
        /// <returns></returns>
        [HttpGet("checkunique")]
        public ActionResult<CheckUniqueResponse> CheckUniqueOrganization(string organizationName)
        {
            if (string.IsNullOrEmpty(organizationName))
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = OrganizationMessageList.REQUIRED_ORGANIZATION_NAME
                });
            }

            var isUniqueResult = _organizationService.CheckUniqueOrgName(organizationName);

            return Ok(isUniqueResult);
        }

        /// <summary>
        /// Update organization
        /// </summary>
        /// <param name="editOrganization"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<OrganizationResponse>> Update(EditOrganizationRequest editOrganization)
        {
            var isExistedId = _organizationService.CheckExistingId(editOrganization.OrgId);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = OrganizationMessageList.NOT_FOUND_ORGANIZATION_ID
                });
            }

            var isUnique = _organizationService.CheckUniqueOrgName(editOrganization.OrgName, editOrganization.OrgId);
            if (!isUnique.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = OrganizationMessageList.EXISTED_ORGANIZATION_STRING
                });
            }

            var updatedOrganization = await _organizationService.UpdateAsync(editOrganization, Account.UserId);
            if (updatedOrganization == null)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = CommonMessageList.COMMON_ERROR_MESSAGE
                });
            }

            return Ok(updatedOrganization);
        }

        /// <summary>
        /// Delete Organization
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpDelete("{orgId}")]
        public async Task<ActionResult> Delete(int orgId)
        {
            var isExistedId = _organizationService.CheckExistingId(orgId);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = OrganizationMessageList.NOT_FOUND_ORGANIZATION_ID
                });
            }

            var canDelete = _organizationService.CheckCanDeleting(orgId);
            if (!canDelete)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = OrganizationMessageList.CAN_NOT_DELETE_ORGANIZATION
                });
            }

            await _organizationService.DeleteAsync(orgId);

            return Ok();
        }
    }
}
