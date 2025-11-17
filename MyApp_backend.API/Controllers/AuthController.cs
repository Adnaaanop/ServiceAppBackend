using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs;
using MyApp_backend.Application.DTOs.Authentication;
using MyApp_backend.Application.Interfaces;

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
                return BadRequest(new ApiResponse<object>(false, null, "Failed to send OTP"));

            return Ok(new ApiResponse<object>(true, null, "OTP sent successfully"));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] VerifyOtpRequestDto request)
        {
            var result = await _authService.ResetPasswordWithOtpAsync(request.Email, request.Otp, request.NewPassword, request.ConfirmPassword);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<object>(false, null, "Password reset failed"));

            return Ok(new ApiResponse<object>(true, null, "Password reset successful"));
        }
    }
}
