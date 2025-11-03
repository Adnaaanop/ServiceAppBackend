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
        Task<Result> ResetPasswordWithOtpAsync(string email, string otp, string newPassword, string confirmPassword);

    }
}
