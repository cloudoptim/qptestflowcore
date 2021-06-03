using System.Collections.Generic;
using System.Threading.Tasks;
using QPCore.Data.BaseEntites;
using QPCore.Data.Enitites;
using QPCore.Model.Common;

namespace QPCore.Service.Interfaces
{
    public interface IBaseService<TEntity, TResponseEntity, TCreateEntity, TEditEntity> where TEntity : BaseEntity, new()
                                        where TResponseEntity : class, new()
                                        where TCreateEntity : class, new()
                                        where TEditEntity : class, new()
    {
        ExistedResponse CheckExistedId(int id);

        ExistedResponse CheckExistedName(string name, int? id = null);

        List<TResponseEntity> GetAll();

        TResponseEntity GetById(int id);

        Task<TResponseEntity> CreateAsync(TCreateEntity entity, int userId);

        Task<TResponseEntity> EditAsync(TEditEntity entity, int userId);

        Task DeleteAsync(int id);
    }
}