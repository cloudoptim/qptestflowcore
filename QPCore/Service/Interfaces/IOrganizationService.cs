using QPCore.Model.Common;
using QPCore.Model.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface IOrganizationService
    {
        /// <summary>
        /// Get all organization in system
        /// </summary>
        /// <returns></returns>
        List<OrganizationResponse> GetAll();

        /// <summary>
        /// Get organization by Org Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OrganizationResponse GetById(int id);

        /// <summary>
        /// Check existing organization id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckExistingId(int id);

        /// <summary>
        /// Check delete organization by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True: Can delete. False: Other</returns>
        bool CheckCanDeleting(int id);

        /// <summary>
        /// Create new Organization
        /// </summary>
        /// <param name="createOrganization"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<OrganizationResponse> AddAsync(CreateOrganizationRequest createOrganization, int userId);

        /// <summary>
        /// Edit organization
        /// </summary>
        /// <param name="editOrganization"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<OrganizationResponse> UpdateAsync(EditOrganizationRequest editOrganization, int userId);

        /// <summary>
        /// Check unique organization name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orgId"></param>
        /// <returns>False: Existed Organization name; True: Not Exist</returns>
        CheckUniqueResponse CheckUniqueOrgName(string name, int? orgId = null);

        Task DeleteAsync(int id);
    }
}
