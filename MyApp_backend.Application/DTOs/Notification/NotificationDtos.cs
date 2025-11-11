using MyApp_backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Notification
{
    public class NotificationCreateDto
    {
        public NotificationType Type { get; set; }
        public Guid? RecipientUserId { get; set; }
        public Guid? RecipientProviderId { get; set; }
        public string Message { get; set; }
    }
    public class NotificationResponseDto
    {
        public Guid Id { get; set; }
        public NotificationType Type { get; set; }
        public Guid? RecipientUserId { get; set; }
        public Guid? RecipientProviderId { get; set; }
        public string Message { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateSent { get; set; }
        public DateTime? DateRead { get; set; }
        public int RetryCount { get; set; }
    }
    public class NotificationUpdateDto
    {
        public NotificationStatus Status { get; set; }
        public string Message { get; set; }
    }
}
