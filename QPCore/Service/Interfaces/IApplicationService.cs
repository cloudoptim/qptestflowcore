using QPCore.Model.Applications;
using QPCore.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface IApplicationService
    {
        /// <summary>
        /// Get applications by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<ApplicationResponse> GetUserApplications(int userId);

        /// <summary>
        /// Check unique application name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        CheckUniqueResponse CheckUniqueApplicationName(string name, int? clientId = null);

        /// <summary>
        /// Check existing application id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckExistingId(int id);

        /// <summary>
        /// Check delete application by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True: Can delete. False: Other</returns>
        bool CheckCanDeleting(int id);

        /// <summary>
        /// Create new application
        /// </summary>
        /// <param name="createApplicationRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApplicationResponse> CreateAsync(CreateApplicationRequest createApplicationRequest, int userId);

        /// <summary>
        /// Update application
        /// </summary>
        /// <param name="editApplicationRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApplicationResponse> UpdateAsync(EditApplicationRequest editApplicationRequest, int userId);

        /// <summary>
        /// Get all application
        /// </summary>
        /// <returns></returns>
        List<ApplicationResponse> GetAll();

        /// <summary>
        /// Delete application
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Get application by Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        ApplicationResponse GetById(int clientId);
    }
}
