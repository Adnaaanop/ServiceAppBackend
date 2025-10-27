using MyApp_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
