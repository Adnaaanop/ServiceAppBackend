using AutoMapper;
using MyApp_backend.Application.DTOs.Waranty;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Entities.WarrantySafety;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class WarrantyRequestService : IWarrantyRequestService
    {
        private readonly IWarrantyRequestRepository _repository;
        private readonly IGenericRepository<ApplicationUser> _userRepository; // Use GenericRepository for User
        private readonly IProviderRepository _providerRepository;              // Your custom ProviderRepository
        private readonly IMapper _mapper;

        public WarrantyRequestService(
            IWarrantyRequestRepository repository,
            IGenericRepository<ApplicationUser> userRepository,
            IProviderRepository providerRepository,
            IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        public async Task<WarrantyRequestResponseDto> CreateAsync(WarrantyRequestCreateDto dto)
        {
            // Validate user
            var userExists = await _userRepository.GetByIdAsync(dto.UserId) != null;
            if (!userExists)
                throw new ArgumentException("User does not exist.");

            // Validate provider (custom repo method GetByUserIdAsync)
            var provider = await _providerRepository.GetByUserIdAsync(dto.ProviderId);
            if (provider == null)
                throw new ArgumentException("Provider does not exist.");

            var entity = _mapper.Map<WarrantyRequest>(dto);
            entity.Status = WarrantyStatus.Pending;
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<WarrantyRequestResponseDto>(created);
        }

        public async Task<WarrantyRequestResponseDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<WarrantyRequestResponseDto>(entity);
        }

        public async Task<IEnumerable<WarrantyRequestResponseDto>> GetByUserIdAsync(Guid userId)
        {
            var entities = await _repository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<WarrantyRequestResponseDto>>(entities);
        }

        public async Task<IEnumerable<WarrantyRequestResponseDto>> GetByStatusAsync(WarrantyStatus status)
        {
            var entities = await _repository.GetByStatusAsync(status);
            return _mapper.Map<IEnumerable<WarrantyRequestResponseDto>>(entities);
        }

        public async Task<WarrantyRequestResponseDto> UpdateStatusAsync(Guid id, WarrantyRequestUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            entity.Status = dto.Status;
            entity.Description = dto.Description;
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<WarrantyRequestResponseDto>(updated);
        }
    }
}
