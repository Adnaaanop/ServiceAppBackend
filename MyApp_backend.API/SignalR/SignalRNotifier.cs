using Microsoft.AspNetCore.SignalR;
using MyApp_backend.API.Hubs;
using MyApp_backend.Application.Interfaces;

namespace MyApp_backend.API.SignalR
{
    public class SignalRNotifier : IRealTimeNotifier
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<SignalRNotifier> _logger;

        public SignalRNotifier(
            IHubContext<NotificationsHub> hubContext,
            ILogger<SignalRNotifier> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task NotifyUserAsync(string userId, object notification)
        {
            _logger.LogInformation("[SignalRNotifier] Triggering notification for user: {UserId}. Payload: {Payload}", userId, notification);
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", notification);
            _logger.LogInformation("[SignalRNotifier] Sent notification to user: {UserId}", userId);
        }
    }
}
