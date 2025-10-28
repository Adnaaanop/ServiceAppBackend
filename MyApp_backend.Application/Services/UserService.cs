using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyApp_backend.Application.DTOs.User;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class UserService : IGenericService<UserRequestDto, UserUpdateDto, UserResponseDto, ApplicationUser>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(
            IGenericRepository<ApplicationUser> userRepository,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserResponseDto> CreateAsync(UserRequestDto dto)
        {
            var userEntity = _mapper.Map<ApplicationUser>(dto);
            // TODO: Hash Password before saving if needed
            var created = await _userRepository.AddAsync(userEntity);
            var dtoResult = _mapper.Map<UserResponseDto>(created);
            dtoResult.Roles = (await _userManager.GetRolesAsync(created)).ToList();
            return dtoResult;
        }

        public async Task<UserResponseDto?> GetByIdAsync(Guid id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity == null) return null;
            var dto = _mapper.Map<UserResponseDto>(userEntity);
            dto.Roles = (await _userManager.GetRolesAsync(userEntity)).ToList();
            return dto;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = new List<UserResponseDto>();
            foreach (var user in users)
            {
                var dto = _mapper.Map<UserResponseDto>(user);
                dto.Roles = (await _userManager.GetRolesAsync(user)).ToList();
                userDtos.Add(dto);
            }
            return userDtos;
        }

        public async Task<UserResponseDto?> UpdateAsync(Guid id, UserUpdateDto dto)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity == null) return null;

            _mapper.Map(dto, userEntity);
            userEntity.LastUpdatedAt = DateTime.UtcNow;

            var updated = await _userRepository.UpdateAsync(userEntity);
            var dtoResult = _mapper.Map<UserResponseDto>(updated);
            dtoResult.Roles = (await _userManager.GetRolesAsync(updated)).ToList();
            return dtoResult;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
