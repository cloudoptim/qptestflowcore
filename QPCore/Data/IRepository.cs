﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPCore.Data
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetQuery();

        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(List<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(int id);
    }
}
