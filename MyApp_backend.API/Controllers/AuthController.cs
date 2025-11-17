using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs;
using MyApp_backend.Application.DTOs.Authentication;
using MyApp_backend.Application.Interfaces;
using System.Security.Claims;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (!result.IsSuccess)
                return Unauthorized(new ApiResponse<object>(false, null, "Invalid login credentials"));

            var responseData = new
            {
                result.Token,
                result.RefreshToken,
                result.Errors
            };

            return Ok(new ApiResponse<object>(true, responseData, "Login successful"));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var response = await _authService.RefreshTokenAsync(request);

            if (!response.IsSuccess)
                return Unauthorized(new ApiResponse<object>(false, null, "Invalid refresh token"));

            var responseData = new
            {
                response.Token,
                response.RefreshToken,
                response.Errors
            };

            return Ok(new ApiResponse<object>(true, responseData, "Token refreshed"));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
        {
            var result = await _authService.SendOtpAsync(request.Email);
            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<object>(false, null, string.Join(", ", result.Errors)));

            return Ok(new ApiResponse<object>(true, null, "OTP sent successfully"));
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto request)
        {
            var result = await _authService.VerifyOtpAsync(request.Email, request.Otp);
            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<object>(false, null, string.Join(", ", result.Errors)));

            return Ok(new ApiResponse<object>(true, null, "OTP verified successfully"));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            var result = await _authService.ResetPasswordAsync(request.Email, request.NewPassword, request.ConfirmPassword);
            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<object>(false, null, string.Join(", ", result.Errors)));

            return Ok(new ApiResponse<object>(true, null, "Password reset successfully"));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var result = await _authService.LogoutAsync(userId);
            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<object>(false, null, string.Join(", ", result.Errors)));

            return Ok(new ApiResponse<bool>(true, true, "Logged out successfully"));
        }


    }
}
