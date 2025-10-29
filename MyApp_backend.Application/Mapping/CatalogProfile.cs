using AutoMapper;
using MyApp_backend.Application.DTOs.Catalog;
using MyApp_backend.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Mapping
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<ServiceCategoryCreateDto, ServiceCategory>();
            CreateMap<ServiceCategoryUpdateDto, ServiceCategory>();
            CreateMap<ServiceCategory, ServiceCategoryResponseDto>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<ServiceCreateDto, Service>();
            CreateMap<ServiceUpdateDto, Service>();
            CreateMap<Service, ServiceResponseDto>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));
        }
    }
}
