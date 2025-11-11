using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Notification;
using MyApp_backend.Application.Interfaces;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // POST: api/Notification
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NotificationCreateDto dto)
        {
            var created = await _notificationService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // GET: api/Notification/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null) return NotFound();
            return Ok(notification);
        }

        // GET: api/Notification/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var items = await _notificationService.GetByUserIdAsync(userId);
            return Ok(items);
        }

        // GET: api/Notification/provider/{providerId}
        [HttpGet("provider/{providerId}")]
        public async Task<IActionResult> GetByProviderId(Guid providerId)
        {
            var items = await _notificationService.GetByProviderIdAsync(providerId);
            return Ok(items);
        }

        // PUT: api/Notification/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] NotificationUpdateDto dto)
        {
            var updated = await _notificationService.UpdateStatusAsync(id, dto.Status);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

    }
}
