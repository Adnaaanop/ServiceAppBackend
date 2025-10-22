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
    public class UserService : IGenericService<UserRequestDto, UserUpdateDto, UserResponseDto, User>
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Create a new user
        public async Task<UserResponseDto> CreateAsync(UserRequestDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            entity.PasswordHash = PasswordHelper.HashPassword(dto.Password);
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsActive = true;

            var created = await _repository.AddAsync(entity);
            return _mapper.Map<UserResponseDto>(created);
        }

        // Get all users
        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        // Get user by Id
        public async Task<UserResponseDto?> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserResponseDto>(user);
        }

        // Update user
        public async Task<UserResponseDto?> UpdateAsync(Guid id, UserUpdateDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            // Map only non-null fields from DTO to entity
            _mapper.Map(dto, user);
            user.LastUpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(user);
            return _mapper.Map<UserResponseDto>(updated);
        }

        // Soft delete user
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id); // Soft delete handled in GenericRepository
        }
    }
}
