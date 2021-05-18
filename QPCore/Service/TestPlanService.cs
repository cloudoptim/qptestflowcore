using AutoMapper;
using AutoMapper.QueryableExtensions;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Common;
using QPCore.Model.TestPlans;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class TestPlanService : ITestPlanService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TestPlan> _repository;

        public TestPlanService(IMapper mapper,
            IRepository<TestPlan> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public bool CheckCanDeleting(int id)
        {
            var canDelete = _repository.GetQuery()
                        .Any(p => p.Id == id && (p.Childs.Any()
                                              || p.TestPlanTestCaseAssociations.Any()
                                            ));

            return !canDelete;
        }

        public bool CheckExistedId(int id)
        {
            var query = _repository.GetQuery()
                       .Any(p => p.Id == id);

            return query;
        }

        public CheckUniqueResponse CheckUniqueName(string name, int? id = null)
        {
            name = name.Trim().ToLower();

            var isExisted = _repository.GetQuery()
                        .Any(p => p.Name.ToLower() == name
                                && (!id.HasValue || (id.HasValue && p.Id != id.Value)));

            var result = new CheckUniqueResponse()
            {
                IsUnique = !isExisted
            };

            return result;
        }

        public async Task<TestPlanResponse> CreateAsync(CreateTestPlanRequest createTestPlanRequest, int userId)
        {
            var newTestPlan = _mapper.Map<TestPlan>(createTestPlanRequest);
            newTestPlan.CreatedBy = userId;
            newTestPlan.CreatedDate = DateTime.Now;
            newTestPlan.IsActive = true;
            newTestPlan = await _repository.AddAsync(newTestPlan);

            return GetById(newTestPlan.Id);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public List<TestPlanResponse> GetAll()
        {
            var testPlans = _repository.GetQuery()
                        .Where(p => !p.ParentId.HasValue)
                        .ProjectTo<TestPlanResponse>(_mapper.ConfigurationProvider)
                        .OrderByDescending(p => p.Id)
                        .ToList();

            return testPlans;
        }

        public TestPlanResponse GetById(int id)
        {
            var query = _repository.GetQuery()
                        .Where(p => p.Id == id)
                        .ProjectTo<TestPlanResponse>(_mapper.ConfigurationProvider);
            return query.FirstOrDefault();
        }

        public List<TestPlanResponse> GetByParentId(int parentId)
        {
            var query = _repository.GetQuery()
                        .Where(p => p.ParentId == parentId)
                        .ProjectTo<TestPlanResponse>(_mapper.ConfigurationProvider)
                        .OrderByDescending(p => p.CreatedDate)
                        .ToList();
            return query;
        }

        public async Task<TestPlanResponse> UpdateAsync(EditTestPlanRequest editTestPlanRequest, int userId)
        {
            var testPlan = _repository.GetQuery()
                                .Where(p => p.Id == editTestPlanRequest.Id)
                                .FirstOrDefault();


            if (testPlan != null)
            {
                testPlan.ParentId = editTestPlanRequest.ParentId;
                testPlan.Name = editTestPlanRequest.Name;
                testPlan.IsActive = editTestPlanRequest.IsActive;
                testPlan.AssignTo = editTestPlanRequest.AssignTo;
                testPlan.UpdatedBy = userId;
                testPlan.UpdatedDate = DateTime.Now;

                var updatedTestPlan = await _repository.UpdateAsync(testPlan);
                return GetById(updatedTestPlan.Id);
            }

            return null;
        }
    }
}
