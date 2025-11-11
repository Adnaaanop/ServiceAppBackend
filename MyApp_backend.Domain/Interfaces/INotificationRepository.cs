using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> AddAsync(Notification notification);
        Task<Notification?> GetByIdAsync(Guid id);
        Task<IEnumerable<Notification>> GetByRecipientUserIdAsync(Guid userId);
        Task<IEnumerable<Notification>> GetByRecipientProviderIdAsync(Guid providerId);
        Task<IEnumerable<Notification>> GetByTypeAsync(NotificationType type);
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<Notification> UpdateAsync(Notification notification);
        Task<bool> DeleteAsync(Guid id);
    }
}
