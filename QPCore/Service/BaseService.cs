using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using QPCore.Data;
using QPCore.Data.BaseEntites;
using QPCore.Data.Enitites;
using QPCore.Model.Common;
using QPCore.Service.Interfaces;

namespace QPCore.Service
{
    public class BaseService<TEntity, TResponseEntity, TCreateEntity, TEditEntity> : IBaseService<TEntity, TResponseEntity, TCreateEntity, TEditEntity> where TEntity : BaseEntity, new()
                                        where TResponseEntity : class, new()
                                        where TCreateEntity : class, new()
                                        where TEditEntity : class, new()
    {
        private readonly IBaseRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        public BaseService(IBaseRepository<TEntity> repository,
                IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public ExistedResponse CheckExistedId(int id)
        {
            var result = new ExistedResponse();
            result.IsExisted = this._repository.GetQuery()
                .Any(p => p.Id == id);

            return result;
        }

        public ExistedResponse CheckExistedName(string name, int? id = null)
        {
            var result = new ExistedResponse();
            name = name.Trim().ToLower();
            result.IsExisted = this._repository.GetQuery()
                .Any(p => p.Name.ToLower() == name &&
                        (!id.HasValue || p.Id != id.Value)
                    );
            return result;
        }

        public async Task<TResponseEntity> CreateAsync(TCreateEntity entity, int userId)
        {
            var insertEntity = _mapper.Map<TEntity>(entity);
            insertEntity.CreatedBy = userId;
            insertEntity.CreatedDate = System.DateTime.Now;
            insertEntity.UpdatedBy = userId;
            insertEntity.UpdatedDate = System.DateTime.Now;

            var result = await _repository.AddAsync(insertEntity);

            return GetById(result.Id);
        }

        public async Task DeleteAsync(int id)
        {
            await this._repository.DeleteAsync(id);
        }

        public async Task<TResponseEntity>  EditAsync(TEditEntity entity, int userId)
        {
            var insertEntity = _mapper.Map<TEntity>(entity);
            insertEntity.UpdatedBy = userId;
            insertEntity.UpdatedDate = System.DateTime.Now;

            var result = await _repository.UpdateAsync(insertEntity);

            return GetById(result.Id);
        }

        public List<TResponseEntity> GetAll()
        {
            var result = this._repository.GetQuery()
                .OrderByDescending(p => p.Id)
                .ProjectTo<TResponseEntity>(_mapper.ConfigurationProvider)
                .ToList();

            return result;
        }

        public TResponseEntity GetById(int id)
        {
            var result = _repository.GetQuery()
                .Where(p => p.Id == id)
                .ProjectTo<TResponseEntity>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
            
            return result;
        }
    }
}