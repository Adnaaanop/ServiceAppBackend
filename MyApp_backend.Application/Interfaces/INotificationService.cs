using MyApp_backend.Domain.Enums;
using MyApp_backend.Application.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResponseDto> CreateAsync(NotificationCreateDto dto);
        Task<NotificationResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<NotificationResponseDto>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<NotificationResponseDto>> GetByProviderIdAsync(Guid providerId);
        Task<NotificationResponseDto> UpdateStatusAsync(Guid id, NotificationStatus status);
    }
}
