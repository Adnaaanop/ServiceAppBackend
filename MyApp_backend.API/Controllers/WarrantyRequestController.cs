using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Waranty;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities.WarrantySafety;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyRequestController : ControllerBase
    {
        private readonly IWarrantyRequestService _warrantyRequestService;

        public WarrantyRequestController(IWarrantyRequestService warrantyRequestService)
        {
            _warrantyRequestService = warrantyRequestService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WarrantyRequestCreateDto dto)
        {
            var created = await _warrantyRequestService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var warrantyRequest = await _warrantyRequestService.GetByIdAsync(id);
            if (warrantyRequest == null) return NotFound();
            return Ok(warrantyRequest);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var requests = await _warrantyRequestService.GetByUserIdAsync(userId);
            return Ok(requests);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(WarrantyStatus status)
        {
            var requests = await _warrantyRequestService.GetByStatusAsync(status);
            return Ok(requests);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] WarrantyRequestUpdateDto dto)
        {
            var updated = await _warrantyRequestService.UpdateStatusAsync(id, dto);
            if (updated == null) return NotFound();
            return NoContent();
        }
    }
}
