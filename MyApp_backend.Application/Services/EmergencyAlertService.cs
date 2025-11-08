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
    public class EmergencyAlertService : IEmergencyAlertService
    {
        private readonly IEmergencyAlertRepository _repository;
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;

        public EmergencyAlertService(
            IEmergencyAlertRepository repository,
            IGenericRepository<ApplicationUser> userRepository,
            IProviderRepository providerRepository,
            IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        public async Task<EmergencyAlertResponseDto> CreateAsync(EmergencyAlertCreateDto dto)
        {
            // Validate userId
            var userExists = await _userRepository.GetByIdAsync(dto.UserId) != null;
            if (!userExists)
                throw new ArgumentException("User does not exist.");

            // Validate providerId (using your repo's GetByUserIdAsync)
            var provider = await _providerRepository.GetByUserIdAsync(dto.ProviderId);
            if (provider == null)
                throw new ArgumentException("Provider does not exist.");

            var entity = _mapper.Map<EmergencyAlert>(dto);
            entity.AlertTime = DateTime.UtcNow;
            entity.IsHandled = false;
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<EmergencyAlertResponseDto>(created);
        }

        public async Task<EmergencyAlertResponseDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<EmergencyAlertResponseDto>(entity);
        }

        public async Task<IEnumerable<EmergencyAlertResponseDto>> GetActiveAlertsAsync()
        {
            var entities = await _repository.GetActiveAlertsAsync();
            return _mapper.Map<IEnumerable<EmergencyAlertResponseDto>>(entities);
        }

        public async Task<EmergencyAlertResponseDto> MarkAsHandledAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            entity.IsHandled = true;
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<EmergencyAlertResponseDto>(updated);
        }
    }
}
