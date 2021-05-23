using System.Collections.Generic;
using System.Threading.Tasks;
using QPCore.Model.TestFlowCategoryAssocs;

namespace QPCore.Service.Interfaces
{
    public interface ITestFlowCategoryAssocService
    {
        /// <summary>
        /// Get all associations by category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        List<TestFlowCategoryAssocResponse> GetByCategoryId(int categoryId);

        /// <summary>
        /// Get all associations by test flow id
        /// </summary>
        /// <param name="testFlowId"></param>
        /// <returns></returns>
        List<TestFlowCategoryAssocResponse> GetByTestFlowId(int testFlowId);

        /// <summary>
        /// Get by associate id
        /// </summary>
        /// <param name="associateId"></param>
        /// <returns></returns>
        TestFlowCategoryAssocResponse GetById(int associateId);

        /// <summary>
        /// Delete association
        /// </summary>
        /// <param name="associateId"></param>
        /// <returns></returns>
        Task DeleteAsync(int associateId);

        /// <summary>
        /// Create new assignment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TestFlowCategoryAssocResponse> AddAsync(CreateTestFlowCategoryAssocRequest model);

        /// <summary>
        /// Check existed id
        /// </summary>
        /// <param name="associateId"></param>
        /// <returns></returns>
        bool CheckExistedId(int associateId);
        
        /// <summary>
        /// Check existed association
        /// </summary>
        /// <param name="testFlowId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        bool CheckExistedAssociation(int testFlowId, int categoryId);
    }
}