using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Catalog;
using MyApp_backend.Application.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyApp_backend.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        // GET: api/Service
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _service.GetAllAsync();
            return Ok(services);
        }

        // GET: api/Service/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var service = await _service.GetByIdAsync(id);
            if (service == null) return NotFound();
            return Ok(service);
        }

        // GET: api/Service/my  (provider's own services)
        [Authorize(Roles = "Provider")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyServices()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            var providerId = Guid.Parse(userIdString); // Service.ProviderId == ApplicationUser.Id

            var services = await _service.GetByProviderIdAsync(providerId);

            // Hide soft-deleted services from the provider UI
            var activeServices = services.Where(s => !s.IsDeleted);

            return Ok(activeServices);
        }


        // POST: api/Service
        [Authorize(Roles = "Provider")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ServiceCreateDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            dto.ProviderId = Guid.Parse(userIdString); // ensure ProviderId is set

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                // temporary: surface error while debugging
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Failed to create service",
                    Error = ex.Message,
                    ex.StackTrace
                });
            }
        }

        // PUT: api/Service/{id}
        [Authorize(Roles = "Provider")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] ServiceUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/Service/{id}
        [Authorize(Roles = "Provider")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
