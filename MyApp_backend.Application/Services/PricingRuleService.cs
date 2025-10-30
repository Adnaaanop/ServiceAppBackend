using AutoMapper;
using MyApp_backend.Application.DTOs.Payment;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities.Payment;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class PricingRuleService : IGenericService<PricingRuleCreateDto, PricingRuleUpdateDto, PricingRuleResponseDto, PricingRule>
    {
        private readonly IGenericRepository<PricingRule> _repository;
        private readonly IMapper _mapper;

        public PricingRuleService(IGenericRepository<PricingRule> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PricingRuleResponseDto> CreateAsync(PricingRuleCreateDto dto)
        {
            var entity = _mapper.Map<PricingRule>(dto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<PricingRuleResponseDto>(createdEntity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PricingRuleResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<PricingRuleResponseDto>>(entities);
        }

        public async Task<PricingRuleResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<PricingRuleResponseDto>(entity);
        }

        public async Task<PricingRuleResponseDto?> UpdateAsync(Guid id, PricingRuleUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<PricingRuleResponseDto>(entity);
        }
    }
}
