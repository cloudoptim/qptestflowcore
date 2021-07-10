using QPCore.Data.Enitites;
using QPCore.Model.WorkItemTypes;

namespace QPCore.Service.Interfaces
{
    public interface IWorkItemTypeService : IBaseService<WorkItemType, WorkItemTypeResponse, CreateWorkItemTypeRequest, EditWorkItemTypeRequest>
    {
    }
}