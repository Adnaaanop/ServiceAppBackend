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
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(dest => dest.RazorpayOrderId, opt => opt.MapFrom(src => src.RazorpayOrderId))
                .ForMember(dest => dest.RazorpayPaymentId, opt => opt.MapFrom(src => src.RazorpayPaymentId))
                .ForMember(dest => dest.RazorpaySignature, opt => opt.MapFrom(src => src.RazorpaySignature));

            CreateMap<PaymentCreateDto, Payment>()
                .ForMember(dest => dest.RazorpayOrderId, opt => opt.MapFrom(src => src.RazorpayOrderId))
                .ForMember(dest => dest.RazorpayPaymentId, opt => opt.MapFrom(src => src.RazorpayPaymentId))
                .ForMember(dest => dest.RazorpaySignature, opt => opt.MapFrom(src => src.RazorpaySignature));

            CreateMap<PaymentUpdateDto, Payment>()
                .ForMember(dest => dest.RazorpayPaymentId, opt => opt.MapFrom(src => src.RazorpayPaymentId))
                .ForMember(dest => dest.RazorpaySignature, opt => opt.MapFrom(src => src.RazorpaySignature));
        }
    }
}
