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
            CreateMap<Booking, BookingResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<BookingCreateDto, Booking>()
                // On create, status defaults to Pending in your service, so not mapped here
                .ForMember(dest => dest.Status, opt => opt.Ignore());

            CreateMap<BookingUpdateDto, Booking>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
