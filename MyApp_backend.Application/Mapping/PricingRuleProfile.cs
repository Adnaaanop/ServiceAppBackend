using AutoMapper;
using MyApp_backend.Application.DTOs.Payment;
using MyApp_backend.Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Mapping
{
    public class PricingRuleProfile : Profile
    {
        public PricingRuleProfile()
        {
            CreateMap<PricingRule, PricingRuleResponseDto>();
            CreateMap<PricingRuleCreateDto, PricingRule>();
            CreateMap<PricingRuleUpdateDto, PricingRule>();
        }
    }
}
