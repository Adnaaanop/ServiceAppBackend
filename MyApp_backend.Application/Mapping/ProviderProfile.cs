//using AutoMapper;
//using MyApp_backend.Application.DTOs.Provider;
//using MyApp_backend.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyApp_backend.Application.Mapping
//{
//    public class ProviderProfile : Profile
//    {
//        public ProviderProfile()
//        {
//            // Map from ProviderRequestDto to Provider entity
//            CreateMap<ProviderRequestDto, Provider>()
//                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID will be set in service
//                .ForMember(dest => dest.VerificationStatus, opt => opt.Ignore()) // default handled in entity/service
//                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // handled in service
//                .ForMember(dest => dest.IsActive, opt => opt.Ignore()) // handled in service
//                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
//            // Only map non-null fields

//            // Map from ProviderUpdateDto to Provider entity
//            CreateMap<ProviderUpdateDto, Provider>()
//                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
//            // Only update fields provided by client

//            // Map Provider entity to ProviderResponseDto
//            CreateMap<Provider, ProviderResponseDto>()
//                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
//                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
//                .ForMember(dest => dest.LastUpdatedAt, opt => opt.MapFrom(src => src.LastUpdatedAt));

//            // Map Provider entity to ProviderMinimalDto
//            CreateMap<Provider, ProviderMinimalDto>()
//                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
//        }
//    }
//}
