using Microsoft.AspNetCore.SignalR;
using MyApp_backend.Application.DTOs.Waranty;

namespace MyApp_backend.API.Hubs
{
    public class EmergencyAlertHub : Hub
    {
        // Method to broadcast new alerts to all connected clients
        public async Task BroadcastAlert(EmergencyAlertResponseDto alert)
        {
            await Clients.All.SendAsync("ReceiveEmergencyAlert", alert);
        }
    }
}
