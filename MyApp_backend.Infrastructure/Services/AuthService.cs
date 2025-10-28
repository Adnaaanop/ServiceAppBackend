using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyApp_backend.Application.DTOs.Authentication;
using MyApp_backend.Application.Helpers;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Application.Models;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Interfaces;
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

        public AuthService(
            UserManager<ApplicationUser> userManager,
            JwtTokenHelper jwtTokenHelper,
            IConfiguration configuration,
            IProviderRepository providerRepository)
        {
            _userManager = userManager;
            _jwtTokenHelper = jwtTokenHelper;
            _configuration = configuration;
            _providerRepository = providerRepository;
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
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // e.g., 7 days expiry
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto { IsSuccess = true, Token = token, RefreshToken = refreshToken };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            Console.WriteLine("==== PROVIDER LOGIN START ====");
            Console.WriteLine($"Received login attempt. Email: {dto.Email}");

            var user = await _userManager.FindByEmailAsync(dto.Email);
            Console.WriteLine(user == null
                ? $"User not found in DB for email: {dto.Email}"
                : $"Fetched user: {user.Email}, Id: {user.Id}");

            if (user == null)
            {
                Console.WriteLine("Login Failed: User not found.");
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid login attempt." } };
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            Console.WriteLine($"Password validity for {user.Email}: {passwordValid}");

            if (!passwordValid)
            {
                Console.WriteLine("Login Failed: Incorrect password.");
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid login attempt." } };
            }

            var roles = await _userManager.GetRolesAsync(user);
            Console.WriteLine($"User roles for {user.Email}: {string.Join(", ", roles)}");

            if (roles.Contains("Provider"))
            {
                Console.WriteLine($"Fetching provider profile for user Id: {user.Id}...");

                var profile = await _providerRepository.GetByUserIdAsync(user.Id);

                Console.WriteLine(profile == null
                    ? $"Provider profile NOT FOUND for UserId: {user.Id}"
                    : $"Provider profile fetched for UserId: {user.Id} | IsApproved={profile.IsApproved}");

                if (profile == null)
                {
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Provider profile not found. Please contact support." }
                    };
                }

                if (!profile.IsApproved)
                {
                    Console.WriteLine("Provider account exists but is NOT approved by admin.");
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Your provider account is not yet approved by admin." }
                    };
                }
            }

            var userModel = await MapToUserModel(user);
            Console.WriteLine("Mapped ApplicationUser to UserModel.");

            var token = _jwtTokenHelper.GenerateJwtToken(userModel);
            Console.WriteLine("JWT Token generated.");

            var refreshToken = _jwtTokenHelper.GenerateRefreshToken();
            Console.WriteLine("Refresh token generated.");

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);
            Console.WriteLine("User refresh token and expiry updated in DB.");

            Console.WriteLine($"User {user.Email} logged in successfully.");

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

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var principal = GetPrincipalFromExpiredToken(request.Token);
            if (principal == null) return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid token" } };

            var userId = Guid.Parse(principal.FindFirstValue(JwtRegisteredClaimNames.Sub));
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid refresh request" } };
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
                ValidateLifetime = false // We check expired tokens here, so ignore lifetime validation
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

    }
}
