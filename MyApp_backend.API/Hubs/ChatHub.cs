using Microsoft.AspNetCore.SignalR;
using MyApp_backend.Application.DTOs.Message;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities;

namespace MyApp_backend.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IGenericService<MessageCreateDto, object, MessageResponseDto, Message> _messageService;

        public ChatHub(IGenericService<MessageCreateDto, object, MessageResponseDto, Message> messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessage(string user, string message, Guid bookingId, Guid senderId, Guid receiverId)
        {
            var dto = new MessageCreateDto
            {
                BookingId = bookingId,
                SenderId = senderId,
                ReceiverId = receiverId,
                MessageText = message,
            };

            var saved = await _messageService.CreateAsync(dto);

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
