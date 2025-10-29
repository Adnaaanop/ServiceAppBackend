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
    public class ServiceCategoryService : IGenericService<ServiceCategoryCreateDto, ServiceCategoryUpdateDto, ServiceCategoryResponseDto, ServiceCategory>
    {
        private readonly IGenericRepository<ServiceCategory> _repository;
        private readonly IMapper _mapper;

        public ServiceCategoryService(IGenericRepository<ServiceCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceCategoryResponseDto> CreateAsync(ServiceCategoryCreateDto dto)
        {
            var entity = _mapper.Map<ServiceCategory>(dto);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<ServiceCategoryResponseDto>(created);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ServiceCategoryResponseDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceCategoryResponseDto>>(list);
        }

        public async Task<ServiceCategoryResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<ServiceCategoryResponseDto>(entity);
        }

        public async Task<ServiceCategoryResponseDto?> UpdateAsync(Guid id, ServiceCategoryUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<ServiceCategoryResponseDto>(updated);
        }
    }
}
