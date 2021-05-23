using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QPCore.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly QPContext QPDataContext;

        public Repository(QPContext qpDataContext)
        {
            QPDataContext = qpDataContext;
        }

        public IQueryable<TEntity> GetQuery()
        {
            try
            {
                return QPDataContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await QPDataContext.AddAsync(entity);
                await QPDataContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await QPDataContext.AddRangeAsync(entities);
                await QPDataContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entities)} could not be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                QPDataContext.Update(entity);
                await QPDataContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await QPDataContext.Set<TEntity>().FindAsync(id);
                if (entity != null)
                {
                    QPDataContext.Remove(entity);
                   await QPDataContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{typeof(TEntity).Name} could not be deleted: {ex.Message}");
            }
        }

        public void ExecuteNonQuery(string query)
        {
            try
            {
                QPDataContext.Database.ExecuteSqlRaw(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"{typeof(TEntity).Name} could not be deleted: {ex.Message}");
            }
        }

        public async Task ExecuteNonQueryAsync(string query)
        {
            try
            {
                await QPDataContext.Database.ExecuteSqlRawAsync(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"{typeof(TEntity).Name} could not be deleted: {ex.Message}");
            }
        }
    }
}
