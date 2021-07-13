using System.Collections.Generic;
using AutoMapper;
using QPCore.Data.Enitites;
using QPCore.Model.WorkItems;
using QPCore.Model.WorkItemTestcaseAssoc;
using QPCore.Service.Interfaces;
using System.Linq;
using AutoMapper.QueryableExtensions;
using QPCore.Model.DataBaseModel.TestFlows;
using QPCore.Data;

namespace QPCore.Service
{
    public class WorkItemTestcaseAssocService : BaseService<WorkItemTestcaseAssoc, WorkItemTestcaseAssocResponse, CreateWorkItemTestcaseAssocRequest, EditWorkItemTestcaseAssocRequest>, IWorkItemTestcaseAssocService
    {
        private readonly IRepository<OrgUser> _orgUserRepository;
        public WorkItemTestcaseAssocService(Data.IBaseRepository<WorkItemTestcaseAssoc> repository, IMapper mapper, IRepository<OrgUser> orgUserRepository) 
            : base(repository, mapper)
        {
            _orgUserRepository = orgUserRepository;
        }

        public bool CheckUniqueAssignment(int testcaseId, int workItemId)
        {
            var isExisted = this.Repository.GetQuery()
                .Any(p => p.TestcaseId == testcaseId && p.WorkItemId == workItemId);
            
            return isExisted;
        }

        public List<TestFlowItemResponse> GetTestcaseByWorkItemId(int workItemId)
        {
            var result = this.Repository.GetQuery()
                .Where(p => p.WorkItemId == workItemId)
                .Select(p => p.Testcase)
                .Join(_orgUserRepository.GetQuery(), l => l.LastUpdatedUserId, r => r.UserId, (l, r) => new TestFlowItemResponse()
                {
                    TestFlowId = l.TestFlowId,
                    TestFlowName = l.TestFlowName,
                    TestFlowDescription = l.TestFlowDescription,
                    LastUpdatedUser = r.FirstName + " " + r.LastName,
                    Islocked = l.Islocked ?? false,
                    IsActive = l.IsActive ?? false,
                    LockedBy = l.LockedBy,
                    TestFlowStatus = l.TestFlowStatus,
                    AssignedDatetTime = l.AssignedDatetTime,
                    AssignedTo = l.AssignedTo,
                    ClientId = l.ClientId,
                    LastUpdatedUserId = l.LastUpdatedUserId.Value,
                    LastUpdatedDateTime = l.LastUpdatedDateTime.HasValue ? l.LastUpdatedDateTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : null,
                    SourceFeatureId = l.SourceFeatureId ?? 1,
                    SourceFeatureName = l.SourceFeatureName
                })
                .OrderByDescending(p => p.TestFlowId)
                .ToList();

            return result;
        }

        public List<WorkItemResponse> GetWorkItemsByTestcaseId(int testcaseId)
        {
            var result = this.Repository.GetQuery()
                .Where(p => p.TestcaseId == testcaseId)
                .Select(p => p.WorkItem)
                .OrderByDescending(p => p.Id)
                .ProjectTo<WorkItemResponse>(this.Mapper.ConfigurationProvider)
                .ToList();

            return result;
        }
    }
}