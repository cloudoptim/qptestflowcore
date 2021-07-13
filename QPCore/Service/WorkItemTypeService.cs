using AutoMapper;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.WorkItemTypes;
using QPCore.Service.Interfaces;

namespace QPCore.Service
{
    public class WorkItemTypeService : BaseService<WorkItemType, WorkItemTypeResponse, CreateWorkItemTypeRequest, EditWorkItemTypeRequest>, IWorkItemTypeService
    {
        public WorkItemTypeService(IBaseRepository<WorkItemType> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}