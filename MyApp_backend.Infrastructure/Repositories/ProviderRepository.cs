using Microsoft.EntityFrameworkCore;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Interfaces;
using MyApp_backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Infrastructure.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly MyAppDbContext _context;
        private readonly DbSet<ProviderProfile> _dbSet;

        public ProviderRepository(MyAppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<ProviderProfile>();
        }

        public async Task<ProviderProfile> AddAsync(ProviderProfile entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ProviderProfile?> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<IEnumerable<ProviderProfile>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<ProviderProfile?> UpdateAsync(ProviderProfile entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var entity = await GetByUserIdAsync(userId);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
