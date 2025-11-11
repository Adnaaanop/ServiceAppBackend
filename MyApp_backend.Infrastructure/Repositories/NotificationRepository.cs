using Microsoft.EntityFrameworkCore;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Enums;
using MyApp_backend.Domain.Interfaces;
using MyApp_backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly MyAppDbContext _context;
        private readonly DbSet<Notification> _dbSet;

        public NotificationRepository(MyAppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Notification>();
        }

        public async Task<Notification> AddAsync(Notification notification)
        {
            notification.DateCreated = DateTime.UtcNow;
            await _dbSet.AddAsync(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetByRecipientUserIdAsync(Guid userId)
        {
            return await _dbSet.Where(n => n.RecipientUserId == userId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetByRecipientProviderIdAsync(Guid providerId)
        {
            return await _dbSet.Where(n => n.RecipientProviderId == providerId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetByTypeAsync(NotificationType type)
        {
            return await _dbSet.Where(n => n.Type == type).AsNoTracking().ToListAsync();
        }

        public async Task<Notification> UpdateAsync(Notification notification)
        {
            _dbSet.Update(notification);
            await _context.SaveChangesAsync();
            return notification;
        }
    }
}
