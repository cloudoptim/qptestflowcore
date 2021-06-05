using System.Linq;
using AutoMapper;
using QPCore.Data;
using QPCore.Data.Enitites;
using QPCore.Model.Common;
using QPCore.Service.Interfaces;

namespace QPCore.Service
{
    public class BaseGroupService<TParentEntity, TResponseEntity, TCreateEntity, TEditEntity> : BaseService<TParentEntity, TResponseEntity, TCreateEntity, TEditEntity>, IBaseGroupService<TParentEntity, TResponseEntity, TCreateEntity, TEditEntity> where TParentEntity : BaseGroupEntity, new()
                                        where TResponseEntity : class, new()
                                        where TCreateEntity : class, new()
                                        where TEditEntity : class, new()
    {
        public BaseGroupService(IBaseRepository<TParentEntity> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public ExistedResponse CheckExistedName(string name, int groupId, int? id = null)
        {
            name = name.Trim().ToLower();

            var existedItem = this.Repository.GetQuery()
                .FirstOrDefault(p => p.Name.Trim().ToLower() == name &&
                    p.GroupId == groupId && 
                    (!id.HasValue || p.Id != id.Value)
                    );
            
            var result = new ExistedResponse()
            {
                IsExisted = existedItem != null,
                ExistedId = existedItem?.Id
            };
            
            return result;
        }
    }
}