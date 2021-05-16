using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Accounts;
using QPCore.Model.Common;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Controllers.Admin
{
    public class UsersController : BaseAdminController
    {
        private readonly IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult<List<AccountResponse>> Get()
        {
            var accounts = _accountService.GetAll();

            return Ok(accounts);
        }

        /// <summary>
        /// Update account
        /// </summary>
        /// <param name="editAccountRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<AccountResponse>> Update(EditAccountRequest editAccountRequest)
        {
            var isExistedId = _accountService.CheckExistedId(editAccountRequest.UserId);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = AccountMessageList.NOT_FOUND_ACCOUNT_ID
                });
            }

            var isEmailExisted = _accountService.CheckExistedEmail(editAccountRequest.Email, Account.UserId);
            if (isEmailExisted)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = AccountMessageList.EMAIL_ADDRESS_HAS_ALREADY
                });
            }

            var updatedAccount = await _accountService.UpdateAsync(editAccountRequest, Account.UserId);

            if (updatedAccount == null)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = CommonMessageList.COMMON_ERROR_MESSAGE
                });
            }

            return Ok(updatedAccount);
        }
    }
}
