using System.Collections.Generic;
using AutoMapper;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.WorkItems;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace QPCore.Service.Interfaces
{
    public class WorkItemService : BaseService<WorkItem, WorkItemResponse, CreateWorkItemRequest, EditWorkItemRequest>, IWorkItemService
    {
        public WorkItemService(IBaseRepository<WorkItem> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }

        public List<WorkItemResponse> GetByAzureFeatureId(int azureFeatureId)
        {
            var result = this.Repository.GetQuery()
                .Where(p => p.AzureFeatureId == azureFeatureId)
                .ProjectTo<WorkItemResponse>(this.Mapper.ConfigurationProvider)
                .OrderByDescending(p => p.Id)
                .ToList();

            return result;
        }

        public List<WorkItemResponse> GetByAzureWorkItemId(int azureWorkItemId)
        {
            var result = this.Repository.GetQuery()
                .Where(p => p.AzureWorkItemId == azureWorkItemId)
                .ProjectTo<WorkItemResponse>(this.Mapper.ConfigurationProvider)
                .OrderByDescending(p => p.Id)
                .ToList();

            return result;
        }

        public List<WorkItemResponse> GetByWorkItemTypeId(int workItemTypeId)
        {
            var result = this.Repository.GetQuery()
                .Where(p => p.WorkItemTypeId == workItemTypeId)
                .ProjectTo<WorkItemResponse>(this.Mapper.ConfigurationProvider)
                .OrderByDescending(p => p.Id)
                .ToList();

            return result;
        }
    }
}