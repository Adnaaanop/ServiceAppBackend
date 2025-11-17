using MyApp_backend.Application.DTOs.Authentication;
using MyApp_backend.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
        string GenerateToken(UserModel user);

        Task<Result> SendOtpAsync(string email);
        // Add a new separate method for OTP verification
        Task<Result> VerifyOtpAsync(string email, string otp);

        // Update reset password method without OTP since that is verified separately
        Task<Result> ResetPasswordAsync(string email, string newPassword, string confirmPassword);

        Task<Result> LogoutAsync(string userId);

    }
}
