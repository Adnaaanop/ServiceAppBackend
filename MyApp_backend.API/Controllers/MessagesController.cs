using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Message;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Application.Services;
using MyApp_backend.Domain.Entities;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IGenericService<MessageCreateDto, object, MessageResponseDto, Message> _messageService;

        public MessagesController(IGenericService<MessageCreateDto, object, MessageResponseDto, Message> messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageCreateDto dto)
        {
            var result = await _messageService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetMessage), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(Guid id)
        {
            var result = await _messageService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<IActionResult> GetMessagesForBooking(Guid bookingId)
        {
            // You may need to add this custom method to your service interface!
            var results = await (_messageService as MessageService)?.GetMessagesByBookingAsync(bookingId);
            return Ok(results);
        }
    }
}
