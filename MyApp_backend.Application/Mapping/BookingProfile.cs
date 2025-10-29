using AutoMapper;
using MyApp_backend.Application.DTOs.Booking;
using MyApp_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Mapping
{

    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingCreateDto, Booking>();
            CreateMap<BookingUpdateDto, Booking>();
            CreateMap<Booking, BookingResponseDto>();
        }
    }
}
