using AutoMapper;
using MyApp_backend.Application.DTOs.Booking;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class BookingService : IBookingService
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
            entity.Status = MyApp_backend.Domain.Enums.BookingStatus.Pending; // Always set Pending on create
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

            _mapper.Map(dto, entity); // BookingUpdateDto.Status is enum, so mapping is direct
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<BookingResponseDto>(updated);
        }

        // ---- CUSTOM ADDED METHODS ----

        public async Task<IEnumerable<BookingResponseDto>> GetByUserIdAsync(Guid userId)
        {
            var list = await _repository.GetAllAsync();
            var filtered = list.Where(b => b.UserId == userId).ToList();
            return _mapper.Map<IEnumerable<BookingResponseDto>>(filtered);
        }

        public async Task<IEnumerable<BookingResponseDto>> GetByProviderIdAsync(Guid providerId)
        {
            var list = await _repository.GetAllAsync();
            var filtered = list.Where(b => b.ProviderId == providerId).ToList();
            return _mapper.Map<IEnumerable<BookingResponseDto>>(filtered);
        }

        public async Task<BookingResponseDto?> UpdateStatusAsync(Guid bookingId, string status)
        {
            var entity = await _repository.GetByIdAsync(bookingId);
            if (entity == null) return null;

            // Safely parse string to enum value
            if (Enum.TryParse<MyApp_backend.Domain.Enums.BookingStatus>(status, out var parsedStatus))
            {
                entity.Status = parsedStatus;
            }
            else
            {
                // Invalid input defaults to Pending or any logic you want
                entity.Status = MyApp_backend.Domain.Enums.BookingStatus.Pending;
            }

            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<BookingResponseDto>(updated);
        }
    }
}
