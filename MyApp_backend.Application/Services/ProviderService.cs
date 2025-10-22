//using AutoMapper;
//using MyApp_backend.Application.DTOs.Provider;
//using MyApp_backend.Application.Interfaces;
//using MyApp_backend.Domain.Entities;
//using MyApp_backend.Domain.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace MyApp_backend.Application.Services
//{
//    public class ProviderService : IGenericService<ProviderRequestDto, ProviderUpdateDto, ProviderResponseDto, Provider>
//    {
//        private readonly IGenericRepository<Provider> _repository;
//        private readonly IMapper _mapper;

//        public ProviderService(IGenericRepository<Provider> repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        // Create a new provider
//        public async Task<ProviderResponseDto> CreateAsync(ProviderRequestDto dto)
//        {
//            var entity = _mapper.Map<Provider>(dto);

//            // Set default values
//            entity.VerificationStatus = "Pending";
//            entity.CreatedAt = DateTime.UtcNow;
//            entity.IsActive = true;

//            var created = await _repository.AddAsync(entity);
//            return _mapper.Map<ProviderResponseDto>(created);
//        }

//        // Get all providers
//        public async Task<IEnumerable<ProviderResponseDto>> GetAllAsync()
//        {
//            var providers = await _repository.GetAllAsync();
//            return _mapper.Map<IEnumerable<ProviderResponseDto>>(providers);
//        }

//        // Get provider by Id
//        public async Task<ProviderResponseDto?> GetByIdAsync(Guid id)
//        {
//            var provider = await _repository.GetByIdAsync(id);
//            return provider == null ? null : _mapper.Map<ProviderResponseDto>(provider);
//        }

//        // Update provider
//        public async Task<ProviderResponseDto?> UpdateAsync(Guid id, ProviderUpdateDto dto)
//        {
//            var provider = await _repository.GetByIdAsync(id);
//            if (provider == null) return null;

//            _mapper.Map(dto, provider); // AutoMapper updates only non-null fields
//            provider.LastUpdatedAt = DateTime.UtcNow;

//            var updated = await _repository.UpdateAsync(provider);
//            return _mapper.Map<ProviderResponseDto>(updated);
//        }

//        // Soft delete provider
//        public async Task<bool> DeleteAsync(Guid id)
//        {
//            return await _repository.DeleteAsync(id);
//        }
//    }
//}
