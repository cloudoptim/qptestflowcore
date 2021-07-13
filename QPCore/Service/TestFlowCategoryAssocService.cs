using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.TestFlowCategoryAssocs;
using QPCore.Service.Interfaces;

namespace QPCore.Service
{
    public class TestFlowCategoryAssocService : ITestFlowCategoryAssocService
    {
        private readonly IRepository<TestFlowCategoryAssoc> _repository;
        private readonly IRepository<TestFlow> _testFlowRepository;
        private readonly IMapper _mapper;

        public TestFlowCategoryAssocService(IRepository<TestFlowCategoryAssoc> repository,
            IRepository<TestFlow> testFlowRepository,
            IMapper mapper)
        {
            _repository = repository;
            _testFlowRepository = testFlowRepository;
            _mapper = mapper;
        }

        public async Task<TestFlowCategoryAssocResponse> AddAsync(CreateTestFlowCategoryAssocRequest model)
        {
            var dataItem = _mapper.Map<TestFlowCategoryAssoc>(model);
            dataItem = await _repository.AddAsync(dataItem);

            return GetById(dataItem.TestFlowCatAssocId);
        }

        public bool CheckExistedId(int associateId)
        {
            var query = _repository.GetQuery()
                .Any(p => p.TestFlowCatAssocId == associateId);

            return query;
        }

        public bool CheckExistedAssociation(int testFlowId, int categoryId)
        {
            var query = _repository.GetQuery()
                .Any(p => p.TestFlowId == testFlowId && p.CategoryId == categoryId);

            return query;
        }

        public async Task DeleteAsync(int associateId)
        {
            await _repository.DeleteAsync(associateId);
        }

        public List<TestFlowCategoryAssocResponse> GetByCategoryId(int categoryId)
        {
            var query = _repository.GetQuery()
                .Where(p => p.CategoryId == categoryId)
                .OrderByDescending(p => p.TestFlowCatAssocId)
                .ProjectTo<TestFlowCategoryAssocResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return query;
        }

        public TestFlowCategoryAssocResponse GetById(int associateId)
        {
            var query = _repository.GetQuery()
                .Where(p => p.TestFlowCatAssocId == associateId)
                .ProjectTo<TestFlowCategoryAssocResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
            return query;
        }

        public List<TestFlowCategoryAssocResponse> GetByTestFlowId(int testFlowId)
        {
            var query = _repository.GetQuery()
                .Where(p => p.TestFlowId == testFlowId)
                .OrderByDescending(p => p.TestFlowCatAssocId)
                .ProjectTo<TestFlowCategoryAssocResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return query;
        }

        public async Task BulkAsync(BulkCreateRequest request)
        {
            var testcaseList = new List<TestFlow>();
            foreach (var testFlowId in request.TestcaseIdList)
            {
                var testcase = _testFlowRepository.GetQuery()
                    .FirstOrDefault(p => p.TestFlowId == testFlowId);

                if (testcase != null)
                {
                    foreach (var categoryId in request.CategoryIdList)
                    {
                        var checkEixsted = _repository.GetQuery()
                            .Any(p => p.TestFlowId == testFlowId && p.CategoryId == categoryId);
                        if (!checkEixsted)
                        {
                            testcase.TestFlowCategoryAssocs.Add(new TestFlowCategoryAssoc()
                            {
                                TestFlowId = testFlowId,
                                CategoryId = categoryId
                            });
                        }

                    }

                    testcaseList.Add(testcase);
                }
            }

            await this._testFlowRepository.UpdateRangeAsync(testcaseList);
        }
    }
}