using AutoMapper;
using MyApp_backend.Application.DTOs.Catalog;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities.Catalog;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class ServiceService : IGenericService<ServiceCreateDto, ServiceUpdateDto, ServiceResponseDto, Service>
    {
        private readonly IGenericRepository<Service> _repository;
        private readonly IMapper _mapper;

        public ServiceService(IGenericRepository<Service> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponseDto> CreateAsync(ServiceCreateDto dto)
        {
            var entity = _mapper.Map<Service>(dto);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<ServiceResponseDto>(created);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ServiceResponseDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceResponseDto>>(list);
        }

        public async Task<ServiceResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<ServiceResponseDto>(entity);
        }

        public async Task<ServiceResponseDto?> UpdateAsync(Guid id, ServiceUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<ServiceResponseDto>(updated);
        }
    }
}
