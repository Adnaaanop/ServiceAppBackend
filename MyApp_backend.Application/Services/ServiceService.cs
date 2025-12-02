using AutoMapper;
using MyApp_backend.Application.DTOs.Catalog;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities.Catalog;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IGenericRepository<Service> _repository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public ServiceService(
            IGenericRepository<Service> repository,
            IMapper mapper,
            ICloudinaryService cloudinaryService // for file uploads
        )
        {
            _repository = repository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        // CREATE
        public async Task<ServiceResponseDto> CreateAsync(ServiceCreateDto dto)
        {
            var entity = _mapper.Map<Service>(dto);

            // Handle media file uploads
            var mediaUrls = new List<string>();
            if (dto.MediaFiles != null)
            {
                foreach (var file in dto.MediaFiles)
                {
                    if (file.Length > 0)
                    {
                        var url = await _cloudinaryService.UploadImageAsync(file);
                        mediaUrls.Add(url);
                    }
                }
            }
            entity.MediaUrlsJson = JsonSerializer.Serialize(mediaUrls);

            var created = await _repository.AddAsync(entity);
            return _mapper.Map<ServiceResponseDto>(created);
        }

        // UPDATE
        public async Task<ServiceResponseDto?> UpdateAsync(Guid id, ServiceUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);

            // Replace media if new files uploaded
            if (dto.MediaFiles != null && dto.MediaFiles.Count > 0)
            {
                var mediaUrls = new List<string>();
                foreach (var file in dto.MediaFiles)
                {
                    if (file.Length > 0)
                    {
                        var url = await _cloudinaryService.UploadImageAsync(file);
                        mediaUrls.Add(url);
                    }
                }
                entity.MediaUrlsJson = JsonSerializer.Serialize(mediaUrls);
            }

            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<ServiceResponseDto>(updated);
        }

        // DELETE
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        // GET ALL
        public async Task<IEnumerable<ServiceResponseDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceResponseDto>>(list);
        }

        // GET BY ID
        public async Task<ServiceResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<ServiceResponseDto>(entity);
        }

        // NEW: GET BY PROVIDER ID (for "My Services")
        public async Task<IEnumerable<ServiceResponseDto>> GetByProviderIdAsync(Guid providerId)
        {
            var list = await _repository.FindAsync(s => s.ProviderId == providerId);
            return _mapper.Map<IEnumerable<ServiceResponseDto>>(list);
        }
    }
}
