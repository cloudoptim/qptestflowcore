using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Accounts;
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

        public ProfileController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpGet("")]
        public ActionResult<AccountResponse> GetProfile()
        {
            // users can get their own account
            var id = Account.UserId;

            var account = _accountService.GetById(id);
            return Ok(account);
        }
    }
}
