using AutoMapper;
using QPCore.Data.Enitites;
using QPCore.Model.WorkItemTestcaseAssoc;
using QPCore.Service.Interfaces;

namespace QPCore.Service
{
    public class WorkItemTestcaseAssocService : BaseService<WorkItemTestcaseAssoc, WorkItemTestcaseAssocResponse, CreateWorkItemTestcaseAssocRequest, EditWorkItemTestcaseAssocRequest>, IWorkItemTestcaseAssocService
    {
        public WorkItemTestcaseAssocService(Data.IBaseRepository<WorkItemTestcaseAssoc> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}