using MyApp_backend.Application.DTOs.Booking;
using MyApp_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Interfaces
{
    public interface IBookingService : IGenericService<BookingCreateDto, BookingUpdateDto, BookingResponseDto, Booking>
    {
        Task<IEnumerable<BookingResponseDto>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<BookingResponseDto>> GetByProviderIdAsync(Guid providerId);
        Task<BookingResponseDto?> UpdateStatusAsync(Guid bookingId, string status);
    }
}
