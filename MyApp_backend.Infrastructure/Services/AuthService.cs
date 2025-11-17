using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyApp_backend.Application.DTOs.Authentication;
using MyApp_backend.Application.Helpers;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Application.Models;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Interfaces;
using MyApp_backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenHelper _jwtTokenHelper;
        private readonly IConfiguration _configuration;
        private readonly IProviderRepository _providerRepository;
        private readonly IEmailService _emailService;
        private readonly MyAppDbContext _context;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            JwtTokenHelper jwtTokenHelper,
            IConfiguration configuration,
            IProviderRepository providerRepository,
            IEmailService emailService,
            MyAppDbContext context,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _jwtTokenHelper = jwtTokenHelper;
            _configuration = configuration;
            _providerRepository = providerRepository;
            _emailService = emailService;
            _context = context;
            _logger = logger;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name,
                IsVerified = false
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            var userModel = await MapToUserModel(user);
            var token = _jwtTokenHelper.GenerateJwtToken(userModel);
            var refreshToken = _jwtTokenHelper.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto { IsSuccess = true, Token = token, RefreshToken = refreshToken };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid login attempt." } };

            var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!passwordValid)
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid login attempt." } };

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Provider"))
            {
                var profile = await _providerRepository.GetByUserIdAsync(user.Id);
                if (profile == null)
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Provider profile not found. Please contact support." }
                    };
                if (!profile.IsApproved)
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Your provider account is not yet approved by admin." }
                    };
            }

            var userModel = await MapToUserModel(user);
            var token = _jwtTokenHelper.GenerateJwtToken(userModel);
            var refreshToken = _jwtTokenHelper.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto { IsSuccess = true, Token = token, RefreshToken = refreshToken };
        }

        public string GenerateToken(UserModel user) => _jwtTokenHelper.GenerateJwtToken(user);

        private async Task<UserModel> MapToUserModel(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return new UserModel
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Name = user.Name,
                Roles = roles
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            // Validate refreshToken param directly, removing request.Token and request.RefreshToken.

            // Optional: you can retrieve the user from refresh token stored in DB

            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid refresh token" } };
            }

            var userModel = await MapToUserModel(user);
            var newJwtToken = _jwtTokenHelper.GenerateJwtToken(userModel);
            var newRefreshToken = _jwtTokenHelper.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto { IsSuccess = true, Token = newJwtToken, RefreshToken = newRefreshToken };
        }



        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is JwtSecurityToken jwtSecurityToken &&
                    jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    return principal;
            }
            catch { }

            return null!;
        }

        public async Task<Result> SendOtpAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new Result { IsSuccess = false, Errors = new List<string> { "User not found." } };

            // Generate OTP
            var otp = new Random().Next(100000, 999999).ToString();

            var userOtp = await _context.UserOtps.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (userOtp == null)
            {
                userOtp = new UserOtp { UserId = user.Id };
                _context.UserOtps.Add(userOtp);
            }

            userOtp.OtpCode = otp;
            userOtp.OtpExpiry = DateTime.UtcNow.AddMinutes(10);
            await _context.SaveChangesAsync();

            var subject = "Your OTP Code";
            var body = $"Your OTP code is {otp}. It will expire in 10 minutes.";
            await _emailService.SendEmailAsync(user.Email, subject, body);

            return new Result { IsSuccess = true, Message = "OTP sent successfully." };
        }

        public async Task<Result> VerifyOtpAsync(string email, string otp)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new Result { IsSuccess = false, Errors = new List<string> { "User not found." } };

            var userOtp = await _context.UserOtps.FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (userOtp == null || userOtp.OtpCode != otp || DateTime.UtcNow > userOtp.OtpExpiry)
                return new Result { IsSuccess = false, Errors = new List<string> { "OTP is invalid or expired." } };

            return new Result { IsSuccess = true, Message = "OTP verified." };
        }

        public async Task<Result> ResetPasswordAsync(string email, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
                return new Result { IsSuccess = false, Errors = new List<string> { "Passwords do not match." } };

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new Result { IsSuccess = false, Errors = new List<string> { "User not found." } };

            // Optionally verify OTP here again or rely on frontend logic token
            // Generate password reset token
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (!result.Succeeded)
                return new Result { IsSuccess = false, Errors = result.Errors.Select(e => e.Description).ToList() };

            // Remove OTP since password reset completed
            var userOtp = await _context.UserOtps.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (userOtp != null)
            {
                _context.UserOtps.Remove(userOtp);
                await _context.SaveChangesAsync();
            }

            return new Result { IsSuccess = true, Message = "Password reset successfully." };
        }

        public async Task<Result> LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new Result { IsSuccess = false, Errors = new List<string> { "User not found." } };

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.MinValue;
            await _userManager.UpdateAsync(user);

            return new Result { IsSuccess = true, Message = "Logged out successfully." };
        }


    }
}
