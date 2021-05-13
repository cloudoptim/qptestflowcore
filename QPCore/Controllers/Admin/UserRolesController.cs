using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Model.Accounts;
using QPCore.Model.Common;
using QPCore.Model.Roles;
using QPCore.Model.UserRoles;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Controllers.Admin
{
    public class UserRolesController : BaseAdminController
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        public UserRolesController(IUserRoleService userRoleService,
            IRoleService roleService,
            IAccountService accountService)
        {
            _userRoleService = userRoleService;
            _accountService = accountService;
            _roleService = roleService;
        }

        /// <summary>
        /// Get user role assignment by id
        /// </summary>
        /// <param name="userroleId"></param>
        /// <returns></returns>
        [HttpGet("{userroleId}")]
        public ActionResult<UserRoleResponse> GetById(int userroleId)
        {
            var assignement = _userRoleService.GetById(userroleId);

            return Ok(assignement);
        }

        /// <summary>
        /// Create new user role assignment
        /// </summary>
        /// <param name="userRoleRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserRoleResponse>> Create(CreateUserRoleRequest userRoleRequest)
        {
            var isExistedRole = _roleService.CheckExistedId(userRoleRequest.RoleId);

            if (!isExistedRole)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = RoleMessageList.NOT_FOUND_ROLE_ID
                });
            }

            var isExistedAccount = _accountService.CheckExistedId(userRoleRequest.UserId);
            if (!isExistedAccount)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = AccountMessageList.NOT_FOUND_ACCOUNT_ID
                });
            }

            var isExistedAssignment = _userRoleService.CheckUnique(userRoleRequest.UserId, userRoleRequest.RoleId);
            if (!isExistedAssignment.IsUnique)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = UserRoleMessageList.ASSIGNMENT_EXISTED_IN_SYSTEM
                });
            }

            var assignment= await _userRoleService.CreateAsync(userRoleRequest, Account.UserId);

            return Ok(assignment);
        }

        /// <summary>
        /// Delete user role assignment
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        [HttpDelete("{userRoleId}")]
        public async Task<ActionResult> Delete(int userRoleId)
        {
            var isExistedId = _userRoleService.CheckExistingId(userRoleId);
            if (!isExistedId)
            {
                return BadRequest(new BadRequestResponse()
                {
                    Message = UserRoleMessageList.NOT_FOUND_USER_ROLE_ID
                });
            }

            await _userRoleService.DeleteAsync(userRoleId);

            return Ok();
        }

        /// <summary>
        /// Get roles by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("users/{userId}")]
        public ActionResult<UserRoleInRoleResponse> GetRolesByUserId(int userId)
        {
            var roles = _userRoleService.GetByUserId(userId);

            return Ok(roles);
        }

        /// <summary>
        /// Get users by roles id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("roles/{roleId}")]
        public ActionResult<UserRoleInUserResponse> GetUsersByRoleId(int roleId)
        {
            var users = _userRoleService.GetByRoleId(roleId);

            return Ok(users);
        }

    }
}
