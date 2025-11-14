using AutoMapper;
using Microsoft.Extensions.Logging;
using MyApp_backend.Application.DTOs.Notification;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Enums;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IRealTimeNotifier _notifier; // INTERFACE, not SignalR
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            INotificationRepository repository,
            IMapper mapper,
            IRealTimeNotifier notifier,
            ILogger<NotificationService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _notifier = notifier;
            _logger = logger;
        }

        public async Task<NotificationResponseDto> CreateAsync(NotificationCreateDto dto)
        {
            _logger.LogInformation("[NotificationService] Creating notification for: UserId={UserId}, ProviderId={ProviderId}", dto.RecipientUserId, dto.RecipientProviderId);

            var entity = _mapper.Map<Notification>(dto);
            entity.DateCreated = DateTime.UtcNow;
            entity.Status = NotificationStatus.Pending;
            var created = await _repository.AddAsync(entity);

            string recipientId = entity.RecipientUserId?.ToString() ?? entity.RecipientProviderId?.ToString();
            if (!string.IsNullOrEmpty(recipientId))
            {
                _logger.LogInformation("[NotificationService] Invoking real-time notifier for: {RecipientId}", recipientId);
                await _notifier.NotifyUserAsync(recipientId, _mapper.Map<NotificationResponseDto>(created));
            }

            return _mapper.Map<NotificationResponseDto>(created);
        }

        // Get notification by Id
        public async Task<NotificationResponseDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<NotificationResponseDto>(entity);
        }

        // Get notifications by user
        public async Task<IEnumerable<NotificationResponseDto>> GetByUserIdAsync(Guid userId)
        {
            var entities = await _repository.GetByRecipientUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NotificationResponseDto>>(entities);
        }

        // Get notifications by provider
        public async Task<IEnumerable<NotificationResponseDto>> GetByProviderIdAsync(Guid providerId)
        {
            var entities = await _repository.GetByRecipientProviderIdAsync(providerId);
            return _mapper.Map<IEnumerable<NotificationResponseDto>>(entities);
        }

        // Update notification status or mark as read
        public async Task<NotificationResponseDto> UpdateStatusAsync(Guid id, NotificationStatus status)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            entity.Status = status;
            if (status == NotificationStatus.Read)
                entity.DateRead = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<NotificationResponseDto>(updated);
        }
    }
}
