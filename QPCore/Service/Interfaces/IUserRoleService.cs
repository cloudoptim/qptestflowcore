using QPCore.Model.Common;
using QPCore.Model.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface IUserRoleService
    {
        /// <summary>
        /// Get roles by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<UserRoleInRoleResponse> GetByUserId(int userId);

        /// <summary>
        /// Get users by role id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<UserRoleInUserResponse> GetByRoleId(int roleId);

        /// <summary>
        /// Get by user role id
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        UserRoleResponse GetById(int userRoleId);

        /// <summary>
        /// Create new user role assignment
        /// </summary>
        /// <param name="createUserRoleRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserRoleResponse> CreateAsync(CreateUserRoleRequest createUserRoleRequest, int userId);

        /// <summary>
        /// Delete user role assignment
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        Task DeleteAsync(int userRoleId);

        /// <summary>
        /// Check unique assignment
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        CheckUniqueResponse CheckUnique(int userId, int roleId, int? userRoleId = null);

        /// <summary>
        /// Check existing user role id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckExistingId(int id);
    }
}
