using AutoMapper;
using MyApp_backend.Application.DTOs.Booking;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class BookingService : IGenericService<BookingCreateDto, BookingUpdateDto, BookingResponseDto, Booking>
    {
        private readonly IGenericRepository<Booking> _repository;
        private readonly IMapper _mapper;

        public BookingService(IGenericRepository<Booking> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookingResponseDto> CreateAsync(BookingCreateDto dto)
        {
            var entity = _mapper.Map<Booking>(dto);
            entity.Status = string.IsNullOrEmpty(entity.Status) ? "Pending" : entity.Status;
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<BookingResponseDto>(created);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookingResponseDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingResponseDto>>(list);
        }

        public async Task<BookingResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<BookingResponseDto>(entity);
        }

        public async Task<BookingResponseDto?> UpdateAsync(Guid id, BookingUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<BookingResponseDto>(updated);
        }
    }
}
