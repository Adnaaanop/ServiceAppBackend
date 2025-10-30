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
    public class ReviewService : IReviewService
    {
        private readonly IGenericRepository<Review> _repository;
        private readonly IMapper _mapper;

        public ReviewService(IGenericRepository<Review> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReviewResponseDto> CreateAsync(ReviewCreateDto dto)
        {
            var entity = _mapper.Map<Review>(dto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<ReviewResponseDto>(createdEntity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ReviewResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<ReviewResponseDto>>(entities);
        }

        public async Task<ReviewResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ReviewResponseDto>(entity);
        }

        public async Task<IEnumerable<ReviewResponseDto>> GetByProviderIdAsync(Guid providerId)
        {
            var entities = await _repository.FindAsync(x => x.ProviderId == providerId && !x.IsDeleted);
            return _mapper.Map<List<ReviewResponseDto>>(entities);
        }

        public async Task<ReviewResponseDto?> UpdateAsync(Guid id, ReviewUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<ReviewResponseDto>(entity);
        }
    }
}
