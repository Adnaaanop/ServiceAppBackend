using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Payment;
using MyApp_backend.Application.Interfaces;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewResponseDto>>> GetAll()
        {
            var reviews = await _reviewService.GetAllAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponseDto>> GetById(Guid id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpGet("provider/{providerId}")]
        public async Task<ActionResult<List<ReviewResponseDto>>> GetByProviderId(Guid providerId)
        {
            var reviews = await _reviewService.GetByProviderIdAsync(providerId);
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewResponseDto>> Create(ReviewCreateDto dto)
        {
            var review = await _reviewService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = review.Id }, review);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewResponseDto>> Update(Guid id, ReviewUpdateDto dto)
        {
            var updatedReview = await _reviewService.UpdateAsync(id, dto);
            if (updatedReview == null) return NotFound();
            return Ok(updatedReview);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _reviewService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
