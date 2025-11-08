using AutoMapper;
using MyApp_backend.Application.DTOs.Waranty;
using MyApp_backend.Domain.Entities.WarrantySafety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Mapping
{
    public class WarrantySafetyProfile : Profile
    {
        public WarrantySafetyProfile()
        {
            CreateMap<WarrantyRequestCreateDto, WarrantyRequest>();
            CreateMap<WarrantyRequestUpdateDto, WarrantyRequest>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<WarrantyRequest, WarrantyRequestResponseDto>();

            CreateMap<EmergencyAlertCreateDto, EmergencyAlert>();
            CreateMap<EmergencyAlert, EmergencyAlertResponseDto>();
        }
    }
}
