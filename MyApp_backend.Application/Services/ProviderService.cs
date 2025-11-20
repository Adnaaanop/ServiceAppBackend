using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp_backend.Application.DTOs.Provider;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public ProviderService(
            IProviderRepository providerRepo,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            ICloudinaryService cloudinaryService)
        {
            _providerRepo = providerRepo;
            _userManager = userManager;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Guid> RegisterAsync(ProviderRegisterDto dto)
        {
            // 1. Create and register the User object
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                EmailConfirmed = false,
                IsActive = true,
                IsVerified = false,
                CreatedAt = DateTime.UtcNow
            };
            var userResult = await _userManager.CreateAsync(user, dto.Password);
            if (!userResult.Succeeded)
                throw new Exception("User registration failed: " +
                                    string.Join(", ", userResult.Errors.Select(e => e.Description)));

            // 2. Assign "Provider" role
            await _userManager.AddToRoleAsync(user, "Provider");

            // 3. Prepare profile entity from DTO fields
            var providerProfile = new ProviderProfile
            {
                UserId = user.Id,
                BusinessName = dto.BusinessName,
                BusinessDescription = dto.BusinessDescription ?? "",
                ServiceCategoriesJson = JsonSerializer.Serialize(dto.ServiceCategories ?? new List<string>()),
                CertificateUrlsJson = JsonSerializer.Serialize(dto.CertificateUrls ?? new List<string>()),
                LicenseUrlsJson = JsonSerializer.Serialize(dto.LicenseUrls ?? new List<string>()),
                DocumentUrlsJson = JsonSerializer.Serialize(dto.DocumentUrls ?? new List<string>()),
                ServiceAreasJson = JsonSerializer.Serialize(dto.ServiceAreas ?? new List<string>()),
                AvailabilityJson = dto.AvailabilityJson,
                IsApproved = false,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                IsActive = true
                // ProfilePhotoUrl is set below
            };

            // 4. Upload and assign profile photo if provided
            if (dto.ProfilePhoto != null && dto.ProfilePhoto.Length > 0)
            {
                var url = await _cloudinaryService.UploadImageAsync(dto.ProfilePhoto);
                providerProfile.ProfilePhotoUrl = url;
            }

            // 5. Save the provider profile
            await _providerRepo.AddAsync(providerProfile);

            // 6. Optionally verify: did profile save?
            var profileCheck = await _providerRepo.GetByUserIdAsync(user.Id);
            if (profileCheck == null)
                throw new Exception("Provider profile creation failed. Please contact support.");

            return user.Id;
        }


        public async Task<ProviderProfileDto?> GetProfileByUserIdAsync(Guid userId)
        {
            var profile = await _providerRepo.GetByUserIdAsync(userId);
            return profile == null ? null : _mapper.Map<ProviderProfileDto>(profile);
        }

        public async Task<bool> UpdateProfileAsync(Guid userId, ProviderUpdateDto dto)
        {
            var profile = await _providerRepo.GetByUserIdAsync(userId);
            if (profile == null) return false;

            profile.BusinessName = dto.BusinessName;
            profile.BusinessDescription = dto.BusinessDescription ?? profile.BusinessDescription;
            profile.ServiceCategoriesJson = JsonSerializer.Serialize(dto.ServiceCategories ?? new List<string>());
            profile.ServiceAreasJson = JsonSerializer.Serialize(dto.ServiceAreas ?? new List<string>());
            profile.AvailabilityJson = dto.AvailabilityJson;

            // Add photo upload logic
            if (dto.ProfilePhoto != null && dto.ProfilePhoto.Length > 0)
            {
                var url = await _cloudinaryService.UploadImageAsync(dto.ProfilePhoto);
                profile.ProfilePhotoUrl = url;
            }

            profile.LastUpdatedAt = DateTime.UtcNow;
            await _providerRepo.UpdateAsync(profile);
            return true;
        }


        public async Task<bool> ApproveAsync(ProviderApprovalDto dto)
        {
            var profile = await _providerRepo.GetByUserIdAsync(dto.UserId);
            if (profile == null) return false;

            profile.IsApproved = dto.IsApproved;
            profile.LastUpdatedAt = DateTime.UtcNow;
            await _providerRepo.UpdateAsync(profile);
            return true;
        }

        public async Task<IEnumerable<ProviderProfileDto>> GetAllProvidersAsync()
        {
            var profiles = await _providerRepo.GetAllAsync();
            return profiles.Select(p => _mapper.Map<ProviderProfileDto>(p));
        }
    }
}
