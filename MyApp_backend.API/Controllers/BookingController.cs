using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Booking;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IGenericService<BookingCreateDto, BookingUpdateDto, BookingResponseDto, Booking> _service;

        public BookingController(IGenericService<BookingCreateDto, BookingUpdateDto, BookingResponseDto, Booking> service)
        {
            _service = service;
        }

        // Get all bookings (Admin can monitor, Provider/User can filter by role)
        [Authorize(Roles = "Admin,Provider,User")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _service.GetAllAsync();
            return Ok(bookings);
        }

        // Get booking by ID
        [Authorize(Roles = "Admin,Provider,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var booking = await _service.GetByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        // Create new booking (Request service)
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // Update booking (modify, reschedule, change status)
        [Authorize(Roles = "User,Provider,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] BookingUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // Cancel (soft delete) a booking
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
