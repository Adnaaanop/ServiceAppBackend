using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Booking;
using MyApp_backend.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        // ---- USER/PROVIDER FILTERED ENDPOINTS ----

        // GET: api/Booking/user/{userId}
        [Authorize(Roles = "User,Admin")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var bookings = await _service.GetByUserIdAsync(userId);
            return Ok(bookings);
        }

        // GET: api/Booking/provider/{providerId}
        [Authorize(Roles = "Provider,Admin")]
        [HttpGet("provider/{providerId}")]
        public async Task<IActionResult> GetByProvider(Guid providerId)
        {
            var bookings = await _service.GetByProviderIdAsync(providerId);
            return Ok(bookings);
        }

        // PUT: api/Booking/{bookingId}/status
        [Authorize(Roles = "Provider,Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] string status)
        {
            var updated = await _service.UpdateStatusAsync(id, status);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // ---- GENERIC CRUD ENDPOINTS ----

        [Authorize(Roles = "Admin,Provider,User")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _service.GetAllAsync();
            return Ok(bookings);
        }

        [Authorize(Roles = "Admin,Provider,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var booking = await _service.GetByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "User,Provider,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] BookingUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = "User,Provider,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
