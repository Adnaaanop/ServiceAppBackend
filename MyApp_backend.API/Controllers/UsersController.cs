using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.User;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGenericService<UserRequestDto, UserUpdateDto, UserResponseDto, ApplicationUser> _userService;

        public UsersController(IGenericService<UserRequestDto, UserUpdateDto, UserResponseDto, ApplicationUser> userService)
        {
            _userService = userService;
        }

        // POST api/user
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRequestDto dto)
        {
            var createdUser = await _userService.CreateAsync(dto);
            if (createdUser == null)
                return BadRequest("Could not create user");

            return Ok(createdUser);
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // GET api/user/me
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out Guid userId))
                return Unauthorized();

            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDto dto)
        {
            var updatedUser = await _userService.UpdateAsync(id, dto);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        // DELETE api/user/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deleted = await _userService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
