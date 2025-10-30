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
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewResponseDto>();
            CreateMap<ReviewCreateDto, Review>();
            CreateMap<ReviewUpdateDto, Review>();
        }
    }
}
