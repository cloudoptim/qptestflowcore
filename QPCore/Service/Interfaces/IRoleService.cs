using QPCore.Model.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface IRoleService
    {
        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        List<RoleResponse> GetAll();

        /// <summary>
        /// Check existed role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckExistedId(int id);
    }
}
