using System.Collections.Generic;
using QPCore.Data.Enitites;
using QPCore.Model.DataBaseModel.TestFlows;
using QPCore.Model.WorkItems;
using QPCore.Model.WorkItemTestcaseAssoc;

namespace QPCore.Service.Interfaces
{
    public interface IWorkItemTestcaseAssocService : IBaseService<WorkItemTestcaseAssoc, WorkItemTestcaseAssocResponse, CreateWorkItemTestcaseAssocRequest, EditWorkItemTestcaseAssocRequest>
    {
        /// <summary>
        /// Check unique workitem and testcase assignment
        /// </summary>
        /// <param name="testcaseId"></param>
        /// <param name="workItemId"></param>
        /// <returns></returns>
        bool CheckUniqueAssignment(int testcaseId, int workItemId);

        /// <summary>
        /// Get work items by testcase id
        /// </summary>
        /// <param name="testcaseId"></param>
        /// <returns></returns>
        List<WorkItemResponse> GetWorkItemsByTestcaseId(int testcaseId);

        /// <summary>
        /// Get testcase by workitem id
        /// </summary>
        /// <param name="workItemId"></param>
        /// <returns></returns>
        List<TestFlowItemResponse> GetTestcaseByWorkItemId(int workItemId);
        
    }
}