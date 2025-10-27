using AutoMapper;
using MyApp_backend.Application.DTOs.User;

using MyApp_backend.Domain.Entities;
using MyApp_backend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserResponseDto>()
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.AddressJson))
           .ForMember(dest => dest.PreferredServices, opt => opt.MapFrom(src => src.PreferredServicesJson))
           .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber)); // <-- THIS LINE

            CreateMap<UserRequestDto, ApplicationUser>()
                .ForMember(dest => dest.AddressJson, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.PreferredServicesJson, opt => opt.MapFrom(src => src.PreferredServices))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone)); // <-- THIS LINE

            CreateMap<UserUpdateDto, ApplicationUser>()
                .ForMember(dest => dest.AddressJson, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.PreferredServicesJson, opt => opt.MapFrom(src => src.PreferredServices))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone)); // <-- THIS LINE
        }
    }
}
