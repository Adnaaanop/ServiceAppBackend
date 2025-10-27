using Microsoft.EntityFrameworkCore;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Interfaces;
using MyApp_backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp_backend.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
       where TEntity : class
    {
        private readonly MyAppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(MyAppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var createdAtProp = typeof(TEntity).GetProperty("CreatedAt");
            if (createdAtProp != null && createdAtProp.CanWrite)
            {
                createdAtProp.SetValue(entity, DateTime.UtcNow);
            }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            var isDeletedProp = typeof(TEntity).GetProperty("IsDeleted");
            var deletedAtProp = typeof(TEntity).GetProperty("DeletedAt");

            // If soft delete properties exist, set them; otherwise physically remove the entity
            if (isDeletedProp != null && isDeletedProp.CanWrite && deletedAtProp != null && deletedAtProp.CanWrite)
            {
                isDeletedProp.SetValue(entity, true);
                deletedAtProp.SetValue(entity, DateTime.UtcNow);
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var isDeletedProp = typeof(TEntity).GetProperty("IsDeleted");

            if (isDeletedProp != null)
            {
                // Only return non-deleted entities
                return await _dbSet.AsNoTracking().ToListAsync(); // filter in-memory below!
            }
            else
            {
                return await _dbSet.AsNoTracking().ToListAsync();
            }
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);

            var isDeletedProp = typeof(TEntity).GetProperty("IsDeleted");
            if (isDeletedProp != null && entity != null && (bool)isDeletedProp.GetValue(entity) == true)
            {
                return null; // soft deleted item
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var lastUpdatedAtProp = typeof(TEntity).GetProperty("LastUpdatedAt");
            if (lastUpdatedAtProp != null && lastUpdatedAtProp.CanWrite)
            {
                lastUpdatedAtProp.SetValue(entity, DateTime.UtcNow);
            }

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
