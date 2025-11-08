using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MyApp_backend.API.Hubs;
using MyApp_backend.Application.DTOs.Waranty;
using MyApp_backend.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyApp_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmergencyAlertController : ControllerBase
    {
        private readonly IEmergencyAlertService _emergencyAlertService;
        private readonly IHubContext<EmergencyAlertHub> _hubContext;

        public EmergencyAlertController(
            IEmergencyAlertService emergencyAlertService,
            IHubContext<EmergencyAlertHub> hubContext)
        {
            _emergencyAlertService = emergencyAlertService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmergencyAlertCreateDto dto)
        {
            var created = await _emergencyAlertService.CreateAsync(dto);

            // After saving, broadcast to all clients
            await _hubContext.Clients.All.SendAsync("ReceiveEmergencyAlert", created);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAlerts()
        {
            var alerts = await _emergencyAlertService.GetActiveAlertsAsync();
            return Ok(alerts);
        }

        [HttpPut("{id}/handle")]
        public async Task<IActionResult> MarkAsHandled(Guid id)
        {
            var updated = await _emergencyAlertService.MarkAsHandledAsync(id);
            if (updated == null) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var alert = await _emergencyAlertService.GetByIdAsync(id);
            if (alert == null) return NotFound();
            return Ok(alert);
        }
    }
}
