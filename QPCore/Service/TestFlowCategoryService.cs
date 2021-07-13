using System.Collections.Generic;
using System.Threading.Tasks;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Common;
using QPCore.Model.TestFlowCategories;
using QPCore.Service.Interfaces;
using AutoMapper;
using System.Linq;
using System;
using AutoMapper.QueryableExtensions;

namespace QPCore.Service
{
    public class TestFlowCategoryService : ITestFlowCategoryService
    {
        private readonly IRepository<TestFlowCategory> _repository;
        private readonly IMapper _mapper;

        public TestFlowCategoryService(IRepository<TestFlowCategory> repository,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public bool CheckCanDeleting(int categoryId)
        {
            var query = _repository.GetQuery()
                .Any(p => p.CategoryId == categoryId && !p.TestFlowCategoryAssocs.Any());

            return query;
        }

        public bool CheckExistedId(int categoryId)
        {
            var query = _repository.GetQuery()
                .Any(p => p.CategoryId == categoryId);

            return query;
        }

        public CheckUniqueResponse CheckUnique(string name, int? categoryId = null)
        {
            var query = _repository.GetQuery()
                .Any(p => p.CategoryName == name &&
                    (!categoryId.HasValue || p.CategoryId != categoryId.Value));

            return new CheckUniqueResponse()
            {
                IsUnique = !query
            };
        }

        public async Task<TestFlowCategoryResponse> AddAsync(CreateTestFlowCategoryRequest createTestFlowCategoryRequest, int userId)
        {
            var dataItem = _mapper.Map<TestFlowCategory>(createTestFlowCategoryRequest);
            dataItem.IsActive = true;
            dataItem.CreatedBy = userId;
            dataItem.CreatedDate = DateTime.Now;
            dataItem.UpdatedBy = userId;
            dataItem.UpdatedDate = DateTime.Now;

            var dateItem = await _repository.AddAsync(dataItem);

            return GetById(dataItem.CategoryId);
        }

        public async Task Delete(int categoryId)
        {
            await _repository.DeleteAsync(categoryId);
        }

        public List<TestFlowCategoryResponse> GetAll()
        {
            var query = _repository.GetQuery()
                    .OrderByDescending(p => p.CategoryId)
                    .ProjectTo<TestFlowCategoryResponse>(_mapper.ConfigurationProvider)
                    .ToList();
            return query;
        }

        public List<TestFlowCategoryResponse> GetByClientId(int clientId)
        {
            var query = _repository.GetQuery()
                    .Where(p => p.ClientId == clientId)
                    .OrderByDescending(p => p.CategoryId)
                    .ProjectTo<TestFlowCategoryResponse>(_mapper.ConfigurationProvider)
                    .ToList();
            return query;
        }

        public TestFlowCategoryResponse GetById(int categoryId)
        {
            var query = _repository.GetQuery()
                    .Where(p => p.CategoryId == categoryId)
                    .ProjectTo<TestFlowCategoryResponse>(_mapper.ConfigurationProvider)
                    .FirstOrDefault();
            return query;
        }

        public async Task<TestFlowCategoryResponse> UpdateAsync(EditTestFlowCategoryRequest editTestFlowCategoryRequest, int userId)
        {
            var dataItem = _repository.GetQuery()
                    .FirstOrDefault(p => p.CategoryId == editTestFlowCategoryRequest.CategoryId);
            if (dataItem != null)
            {
                dataItem.CategoryName = editTestFlowCategoryRequest.CategoryName;
                dataItem.ClientId = editTestFlowCategoryRequest.ClientId;
                dataItem.IsActive = editTestFlowCategoryRequest.IsActive;
                dataItem.Type = editTestFlowCategoryRequest.Type;
                dataItem.UpdatedBy = userId;
                dataItem.UpdatedDate = DateTime.Now;

                await _repository.UpdateAsync(dataItem);
                return GetById(editTestFlowCategoryRequest.CategoryId);
            }

            return null;
        }

        public PaginationResponse<TestFlowCategoryResponse> GetByType(string type, string keyword, int skip, int limit)
        {
            keyword = string.IsNullOrWhiteSpace(keyword)? string.Empty : keyword.Trim().ToLower();

            var query = _repository.GetQuery()
                    .Where(p => p.Type == type && (string.IsNullOrWhiteSpace(keyword) || p.CategoryName.ToLower().Contains(keyword)))
                    .OrderByDescending(p => p.CategoryName)
                    .ProjectTo<TestFlowCategoryResponse>(_mapper.ConfigurationProvider);
            
            var total = query.Count();
            var items = query
                    .Skip(skip)
                    .Take(limit)
                    .ToList();

            var result = new PaginationResponse<TestFlowCategoryResponse>()
            {
                Items = items,
                Total = total
            };

            return result;
        }
    }
}