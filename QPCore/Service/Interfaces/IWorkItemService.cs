using QPCore.Data.Enitites;
using QPCore.Model.WorkItems;
using System.Collections.Generic;

namespace QPCore.Service.Interfaces
{
    public interface IWorkItemService : IBaseService<WorkItem, WorkItemResponse, CreateWorkItemRequest, EditWorkItemRequest>
    {
        /// <summary>
        /// Get work items by azure feature id
        /// </summary>
        /// <param name="azureFeatureId"></param>
        /// <returns></returns>
        List<WorkItemResponse> GetByAzureFeatureId(int azureFeatureId);

        /// <summary>
        /// Get work items by auzre work item it
        /// </summary>
        /// <param name="azureWorkItemId"></param>
        /// <returns></returns>
        List<WorkItemResponse> GetByAzureWorkItemId(int azureWorkItemId);

        /// <summary>
        /// Get work items by work item type id
        /// </summary>
        /// <param name="workItemTypeId"></param>
        /// <returns></returns>
        List<WorkItemResponse> GetByWorkItemTypeId(int workItemTypeId);
    }
}