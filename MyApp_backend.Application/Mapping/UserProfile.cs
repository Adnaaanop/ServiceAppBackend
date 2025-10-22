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
            // Create mappings

            // Create User from request
            CreateMap<UserRequestDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Update user from update DTO
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Map User entity to response DTO
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            // Map User entity to minimal DTO
            CreateMap<User, UserMinimalDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
