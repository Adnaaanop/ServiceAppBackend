using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Provider;
using MyApp_backend.Application.Interfaces;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;
        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        // POST: api/Provider/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] ProviderRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userId = await _providerService.RegisterAsync(dto);
                return Ok(new { UserId = userId });
            }
            catch (Exception ex)
            {
                // Log both ex.Message and ex.InnerException?.Message for details
                var errorMsg = ex.Message;
                var innerMsg = ex.InnerException?.Message;

                // For debugging: log to console, or return in response (not for production)
                Console.WriteLine("Top Level: " + errorMsg);
                if (innerMsg != null)
                    Console.WriteLine("Inner: " + innerMsg);

                // Also include inner message in error response for now
                return BadRequest(new
                {
                    Message = errorMsg,
                    InnerException = innerMsg
                });
            }
        }

        // GET: api/Provider/profile/{userId}
        [HttpGet("profile/{userId:guid}")]
        [Authorize(Roles = "Provider,Admin")]
        public async Task<IActionResult> GetProfileByUserId(Guid userId)
        {
            var profile = await _providerService.GetProfileByUserIdAsync(userId);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        // PUT: api/Provider/profile/{userId}
        [HttpPut("profile/{userId:guid}")]
        [Authorize(Roles = "Provider,Admin")]
        public async Task<IActionResult> UpdateProfile(Guid userId, [FromForm] ProviderUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _providerService.UpdateProfileAsync(userId, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // POST: api/Provider/approve
        [HttpPost("approve-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve([FromBody] ProviderApprovalDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _providerService.ApproveAsync(dto);
            if (!result) return NotFound();
            return Ok();
        }

        // GET: api/Provider/all
        [HttpGet("all-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProviders()
        {
            var providers = await _providerService.GetAllProvidersAsync();
            return Ok(providers);
        }
    }
}

