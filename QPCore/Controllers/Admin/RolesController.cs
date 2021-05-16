using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QPCore.Common;
using QPCore.Model.Roles;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Controllers.Admin
{
    public class RolesController : BaseAdminController
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<RoleResponse>> GetRoles()
        {
            var roles = _roleService.GetAll();
            return Ok(roles);
        }
    }
}
