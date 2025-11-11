using Microsoft.AspNetCore.SignalR;

namespace MyApp_backend.API.Hubs
{
    public class NotificationsHub : Hub
    {
        // Method to send notification to a specific user (by connection ID or user ID via groups)
        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
