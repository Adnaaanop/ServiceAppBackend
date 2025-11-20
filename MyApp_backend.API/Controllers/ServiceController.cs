using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Catalog;
using MyApp_backend.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IGenericService<ServiceCreateDto, ServiceUpdateDto, ServiceResponseDto, Domain.Entities.Catalog.Service> _service;

        public ServiceController(
            IGenericService<ServiceCreateDto, ServiceUpdateDto, ServiceResponseDto, Domain.Entities.Catalog.Service> service)
        {
            _service = service;
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

        [Authorize(Roles = "Provider")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ServiceCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "Provider")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] ServiceUpdateDto dto)
        {
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
