//// MyApp_Backend.Application/Services/AuthService.cs
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using MyApp_backend.Application.DTOs.Authentication;
//using MyApp_backend.Application.Interfaces;
//using MyApp_Backend.Application.DTOs.Authentication;
//using MyApp_Backend.Application.Helpers;
//using MyApp_Backend.Application.Interfaces;
//using MyApp_Backend.Infrastructure.Identity;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace MyApp_Backend.Application.Services
//{
//    public class AuthService : IAuthService
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        private readonly JwtTokenHelper _jwtTokenHelper;

//        public AuthService(
//            UserManager<ApplicationUser> userManager,
//            SignInManager<ApplicationUser> signInManager,
//            JwtTokenHelper jwtTokenHelper)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _jwtTokenHelper = jwtTokenHelper;
//        }

//        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
//        {
//            var user = new ApplicationUser
//            {
//                UserName = registerDto.Email,
//                Email = registerDto.Email,
//                Name = registerDto.Name,
//                IsVerified = false
//            };

//            var result = await _userManager.CreateAsync(user, registerDto.Password);

//            if (!result.Succeeded)
//            {
//                return new AuthResponseDto
//                {
//                    IsSuccess = false,
//                    Errors = result.Errors.Select(e => e.Description).ToList()
//                };
//            }

//            // Optionally assign a role or add claims
//            // await _userManager.AddToRoleAsync(user, "User");

//            var roles = await _userManager.GetRolesAsync(user);
//            var token = _jwtTokenHelper.GenerateJwtToken(user.Id, user.Email!, user.Name, roles);

//            return new AuthResponseDto
//            {
//                IsSuccess = true,
//                Token = token
//            };
//        }

//        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
//        {
//            var user = await _userManager.FindByEmailAsync(loginDto.Email);
//            if (user == null)
//            {
//                return new AuthResponseDto
//                {
//                    IsSuccess = false,
//                    Errors = new List<string> { "Invalid login attempt." }
//                };
//            }

//            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);

//            if (!result.Succeeded)
//            {
//                return new AuthResponseDto
//                {
//                    IsSuccess = false,
//                    Errors = new List<string> { "Invalid login attempt." }
//                };
//            }

//            var roles = await _userManager.GetRolesAsync(user);
//            var token = _jwtTokenHelper.GenerateJwtToken(user.Id, user.Email!, user.Name, roles);

//            return new AuthResponseDto
//            {
//                IsSuccess = true,
//                Token = token
//            };
//        }
//    }
//}
