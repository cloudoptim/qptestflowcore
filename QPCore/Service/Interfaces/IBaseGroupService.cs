using QPCore.Data.Enitites;
using QPCore.Model.Common;

namespace QPCore.Service.Interfaces
{
    public interface IBaseGroupService<TParentEntity, TResponseEntity, TCreateEntity, TEditEntity> : IBaseService<TParentEntity, TResponseEntity, TCreateEntity, TEditEntity> where TParentEntity : BaseGroupEntity, new()
                                        where TResponseEntity : class, new()
                                        where TCreateEntity : class, new()
                                        where TEditEntity : class, new()
    {
        ExistedResponse CheckExistedName(string name, int groupId, int? id = null);
    }
}