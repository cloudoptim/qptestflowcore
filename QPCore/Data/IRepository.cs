using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QPCore.Data.BaseEntites;

namespace QPCore.Data
{
    //[Obsolete("We should be replace by IBaseRepository")]
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetQuery();

        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(List<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(int id);

        void ExecuteNonQuery(string query);

        Task ExecuteNonQueryAsync(string query);
    }

    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        QPContext QPDataContext { get; }
        IQueryable<TEntity> GetQuery();

        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(List<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(int id);

        void ExecuteNonQuery(string query);

        Task ExecuteNonQueryAsync(string query);
    }
}
