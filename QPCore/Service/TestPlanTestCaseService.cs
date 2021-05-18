using AutoMapper;
using AutoMapper.QueryableExtensions;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.TestPlanTestCases;
using QPCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Service
{
    public class TestPlanTestCaseService : ITestPlanTestCaseService
    {
        private readonly IRepository<TestPlanTestCaseAssociation> _repository;
        private readonly IMapper _mapper;
        public TestPlanTestCaseService(IRepository<TestPlanTestCaseAssociation> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public bool CheckExistedAssignment(int testPlanId, int testFlowId)
        {
            var query = _repository.GetQuery()
                    .Any(p => p.TestPlanId == testPlanId && p.TestCaseId == testFlowId);


            return query;
        }

        public bool CheckExistedId(int id)
        {
            var query = _repository.GetQuery()
                    .Any(p => p.Id == id);

            return query;
        }

        public async Task<List<TestPlanTestCaseResponse>> CreateAsync(CreateTestPlanTestCaseRequest createTestPlanTestCaseRequest, int userId)
        {
            var newAssignments = new List<TestPlanTestCaseAssociation>();
            var distinctTestCaseIdList = createTestPlanTestCaseRequest.TestCaseIds.Distinct();
            foreach (var item in distinctTestCaseIdList)
            {
                var isExisted = CheckExistedAssignment(createTestPlanTestCaseRequest.TestPlanId, item);

                if (!isExisted)
                {
                    var newData = new TestPlanTestCaseAssociation();
                    newData.CreatedBy = userId;
                    newData.CreatedDate = DateTime.Now;
                    newData.IsActive = true;
                    newData.TestPlanId = createTestPlanTestCaseRequest.TestPlanId;
                    newData.TestCaseId = item;

                    newAssignments.Add(newData);
                } 
            }

            if (newAssignments.Any())
            {
                await _repository.AddRangeAsync(newAssignments);
            }

            return GetAll(createTestPlanTestCaseRequest.TestPlanId);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public List<TestPlanTestCaseResponse> GetAll(int testPlanId)
        {
            var query = _repository.GetQuery()
                        .Where(p => p.TestPlanId == testPlanId)
                        .ProjectTo<TestPlanTestCaseResponse>(_mapper.ConfigurationProvider)
                        .OrderBy(p => p.TestCaseId)
                        .ToList();

            return query;
        }

        public async Task<TestPlanTestCaseResponse> UpdateAsync̣̣(EditTestPlanTestCaseRequest editTestPlanTestCaseRequest, int userId)
        {
            var assignment = _repository.GetQuery()
                    .FirstOrDefault(p => p.Id == editTestPlanTestCaseRequest.Id);

            if (assignment != null)
            {
                assignment.IsActive = editTestPlanTestCaseRequest.IsActive;
                assignment.UpdatedBy = userId;
                assignment.UpdatedDate = DateTime.Now;

               assignment = await _repository.UpdateAsync(assignment);

                return GetById(assignment.Id);
            }

            return null;
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private TestPlanTestCaseResponse GetById(int id)
        {
            var query = _repository.GetQuery()
                    .Where(p => p.Id == id)
                    .ProjectTo<TestPlanTestCaseResponse>(_mapper.ConfigurationProvider)
                    .FirstOrDefault();

            return query;
        }

        
    }
}
