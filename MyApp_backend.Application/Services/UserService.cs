using AutoMapper;
using MyApp_backend.Application.DTOs.User;
using MyApp_backend.Application.Helpers;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class UserService : IGenericService<UserRequestDto, UserUpdateDto, UserResponseDto, ApplicationUser>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<ApplicationUser> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> CreateAsync(UserRequestDto dto)
        {
            var userEntity = _mapper.Map<ApplicationUser>(dto);
            // TODO: Hash Password before saving
            var created = await _userRepository.AddAsync(userEntity);
            return _mapper.Map<UserResponseDto>(created);
        }

        public async Task<UserResponseDto?> GetByIdAsync(Guid id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity == null) return null;
            return _mapper.Map<UserResponseDto>(userEntity);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => _mapper.Map<UserResponseDto>(user));
        }

        public async Task<UserResponseDto?> UpdateAsync(Guid id, UserUpdateDto dto)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity == null) return null;

            _mapper.Map(dto, userEntity);
            userEntity.LastUpdatedAt = DateTime.UtcNow;

            var updated = await _userRepository.UpdateAsync(userEntity);
            return _mapper.Map<UserResponseDto>(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
