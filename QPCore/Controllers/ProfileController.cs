using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Accounts;
using QPCore.Model.Applications;
using QPCore.Model.Organizations;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IOrganizationService _organizationService;
        private readonly IApplicationService _applicationService;

        public ProfileController(IAccountService accountService, 
            IOrganizationService organizationService,
            IApplicationService applicationService)
        {
            this._accountService = accountService;
            this._organizationService = organizationService;
            this._applicationService = applicationService;
        }

        [HttpGet("")]
        public ActionResult<AccountResponse> GetProfile()
        {
            // users can get their own account
            var id = Account.UserId;

            var account = _accountService.GetById(id);
            return Ok(account);
        }

        [HttpGet("organization")]
        public ActionResult<OrganizationResponse> GetOrganization()
        {
            var orgId = Account.OrgId;
            var org = _organizationService.GetById(orgId);

            return Ok(org);
        }

        [HttpGet("applications")]
        public ActionResult<ApplicationResponse> GetApplications()
        {
            var userId = Account.UserId;
            var applications = _applicationService.GetUserApplications(userId);

            return Ok(applications);
        }
    }
}
