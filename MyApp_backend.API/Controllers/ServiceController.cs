using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Catalog;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Infrastructure.Services; // <-- Add this for CloudinaryService
using System.Text.Json;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IGenericService<ServiceCreateDto, ServiceUpdateDto, ServiceResponseDto, Domain.Entities.Catalog.Service> _service;
        private readonly CloudinaryService _cloudinaryService; // <-- Add CloudinaryService

        public ServiceController(
            IGenericService<ServiceCreateDto, ServiceUpdateDto, ServiceResponseDto, Domain.Entities.Catalog.Service> service,
            CloudinaryService cloudinaryService)
        {
            _service = service;
            _cloudinaryService = cloudinaryService; // <-- Injected CloudinaryService
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _service.GetAllAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var service = await _service.GetByIdAsync(id);
            if (service == null) return NotFound();
            return Ok(service);
        }

        // For file uploads, use [FromForm] to enable form-data requests (with files)
        [Authorize(Roles = "Provider")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ServiceCreateDto dto)
        {
            // Optionally: verify that ProviderId matches logged in user before allowing create

            // NEW: Upload media files to Cloudinary (if any)
            if (dto.MediaFiles != null && dto.MediaFiles.Count > 0)
            {
                var mediaUrls = new List<string>();
                foreach (var file in dto.MediaFiles)
                {
                    if (file.Length > 0)
                    {
                        var url = await _cloudinaryService.UploadImageAsync(file); // <-- Upload file
                        mediaUrls.Add(url);
                    }
                }
                // Save URLs as JSON array in MediaUrlsJson
                dto.MediaUrlsJson = JsonSerializer.Serialize(mediaUrls);
            }

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Provider")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] ServiceUpdateDto dto)
        {
            // NEW: Upload new media files to Cloudinary (if any)
            if (dto.MediaFiles != null && dto.MediaFiles.Count > 0)
            {
                var mediaUrls = new List<string>();
                foreach (var file in dto.MediaFiles)
                {
                    if (file.Length > 0)
                    {
                        var url = await _cloudinaryService.UploadImageAsync(file); // <-- Upload file
                        mediaUrls.Add(url);
                    }
                }
                // Save URLs as JSON array in MediaUrlsJson
                dto.MediaUrlsJson = JsonSerializer.Serialize(mediaUrls);
            }

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

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
