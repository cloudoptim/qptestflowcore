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
            var result = new ExistedResponse();
            name = name.Trim().ToLower();
            result.IsExisted = this.Repository.GetQuery()
                .Any(p => p.Name.Trim().ToLower() == name &&
                    p.GroupId == groupId && 
                    (!id.HasValue || p.Id != id.Value)
                    );
            return result;
        }
    }
}