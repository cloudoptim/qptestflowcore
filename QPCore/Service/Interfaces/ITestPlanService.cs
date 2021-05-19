using QPCore.Model.Common;
using QPCore.Model.TestPlans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface ITestPlanService
    {
        /// <summary>
        /// Check existed test plan id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckExistedId(int id);

        /// <summary>
        /// Check to be able to delete item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckCanDeleting(int id);

        /// <summary>
        /// Check unique test plan
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        CheckUniqueResponse CheckUniqueName(string name, int? parentId = null, int? id = null);

        /// <summary>
        /// Get all test plan
        /// </summary>
        /// <returns></returns>
        List<TestPlanResponse> GetAll();

        /// <summary>
        /// Get Test Plan by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TestPlanResponse GetById(int id);

        /// <summary>
        /// Get all testplans by parent id
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<TestPlanResponse> GetByParentId(int parentId);

        /// <summary>
        /// Create new test plan
        /// </summary>
        /// <param name="createTestPlanRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<TestPlanResponse> CreateAsync(CreateTestPlanRequest createTestPlanRequest, int userId);

        /// <summary>
        /// Update existing TestPlan
        /// </summary>
        /// <param name="editTestPlanRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<TestPlanResponse> UpdateAsync(EditTestPlanRequest editTestPlanRequest, int userId);

        /// <summary>
        /// Delete test plan by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
