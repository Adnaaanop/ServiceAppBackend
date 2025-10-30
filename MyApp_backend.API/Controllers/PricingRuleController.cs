using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Payment;
using MyApp_backend.Application.Interfaces;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingRuleController : ControllerBase
    {
        private readonly IGenericService<PricingRuleCreateDto, PricingRuleUpdateDto, PricingRuleResponseDto, Domain.Entities.Payment.PricingRule> _pricingRuleService;

        public PricingRuleController(IGenericService<PricingRuleCreateDto, PricingRuleUpdateDto, PricingRuleResponseDto, Domain.Entities.Payment.PricingRule> pricingRuleService)
        {
            _pricingRuleService = pricingRuleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PricingRuleResponseDto>>> GetAll()
        {
            var rules = await _pricingRuleService.GetAllAsync();
            return Ok(rules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PricingRuleResponseDto>> GetById(Guid id)
        {
            var rule = await _pricingRuleService.GetByIdAsync(id);
            if (rule == null) return NotFound();
            return Ok(rule);
        }

        [HttpPost]
        public async Task<ActionResult<PricingRuleResponseDto>> Create(PricingRuleCreateDto dto)
        {
            var created = await _pricingRuleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PricingRuleResponseDto>> Update(Guid id, PricingRuleUpdateDto dto)
        {
            var updated = await _pricingRuleService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _pricingRuleService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
