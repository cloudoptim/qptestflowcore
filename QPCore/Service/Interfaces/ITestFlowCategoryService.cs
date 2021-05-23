using System.Collections.Generic;
using QPCore.Model.TestFlowCategories;
using QPCore.Model.Common;
using System.Threading.Tasks;

namespace QPCore.Service.Interfaces
{
    public interface ITestFlowCategoryService
    {
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        List<TestFlowCategoryResponse> GetAll();

        /// <summary>
        /// Get Categories by client id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        List<TestFlowCategoryResponse> GetByClientId(int clientId);

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        TestFlowCategoryResponse GetById(int categoryId);

        /// <summary>
        /// Check existed id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        bool CheckExistedId(int categoryId);

        /// <summary>
        /// Check to delete category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        bool CheckCanDeleting(int categoryId);

        /// <summary>
        /// Check unique name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        CheckUniqueResponse CheckUnique(string name, int? categoryId = null);

        /// <summary>
        /// Create testflow category
        /// </summary>
        /// <param name="createTestFlowCategoryRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<TestFlowCategoryResponse> AddAsync(CreateTestFlowCategoryRequest createTestFlowCategoryRequest, int userId);

        /// <summary>
        /// Edit category 
        /// </summary>
        /// <param name="editTestFlowCategoryRequest"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// 
        Task<TestFlowCategoryResponse> UpdateAsync(EditTestFlowCategoryRequest editTestFlowCategoryRequest, int userId);
        
        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="categoryId"></param>
        Task Delete(int categoryId);
    }
}