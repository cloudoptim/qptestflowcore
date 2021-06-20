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
        protected readonly IBaseRepository<TEntity> Repository;
        protected readonly IMapper Mapper;
        public BaseService(IBaseRepository<TEntity> repository,
                IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }
        public ExistedResponse CheckExistedId(int id)
        {
            var result = new ExistedResponse();
            result.IsExisted = this.Repository.GetQuery()
                .Any(p => p.Id == id);

            return result;
        }

        public ExistedResponse CheckExistedName(string name, int? id = null)
        {
            name = name.Trim().ToLower();
            var existedItem = this.Repository.GetQuery()
                .FirstOrDefault(p => p.Name.ToLower() == name &&
                        (!id.HasValue || p.Id != id.Value)
                    );
            
            var result = new ExistedResponse()
            {
                IsExisted = existedItem != null,
                ExistedId = existedItem?.Id
            };

            return result;
        }

        public async Task<TResponseEntity> CreateAsync(TCreateEntity entity, int userId)
        {
            TEntity insertEntity = ConvertEntity(entity, userId);

            var result = await Repository.AddAsync(insertEntity);

            return GetById(result.Id);
        }

        public async Task DeleteAsync(int id)
        {
            await this.Repository.DeleteAsync(id);
        }

        public virtual async Task<TResponseEntity> EditAsync(TEditEntity entity, int userId)
        {
            var insertEntity = Mapper.Map<TEntity>(entity);
            insertEntity.UpdatedBy = userId;
            insertEntity.UpdatedDate = System.DateTime.Now;

            var result = await this.Repository.UpdateAsync(insertEntity);

            return GetById(result.Id);
        }

        public List<TResponseEntity> GetAll()
        {
            var result = this.Repository.GetQuery()
                .OrderByDescending(p => p.Id)
                .ProjectTo<TResponseEntity>(Mapper.ConfigurationProvider)
                .ToList();

            return result;
        }

        public virtual TResponseEntity GetById(int id)
        {
            var result = this.Repository.GetQuery()
                .Where(p => p.Id == id)
                .ProjectTo<TResponseEntity>(Mapper.ConfigurationProvider)
                .FirstOrDefault();
            
            return result;
        }

        protected virtual TEntity ConvertEntity(TCreateEntity entity, int userId)
        {
            var insertEntity = Mapper.Map<TEntity>(entity);
            insertEntity.CreatedBy = userId;
            insertEntity.CreatedDate = System.DateTime.Now;
            insertEntity.UpdatedBy = userId;
            insertEntity.UpdatedDate = System.DateTime.Now;
            return insertEntity;
        }
    }
}