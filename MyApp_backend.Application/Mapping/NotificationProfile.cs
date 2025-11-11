using AutoMapper;
using MyApp_backend.Application.DTOs.Notification;
using MyApp_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Mapping
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationCreateDto, Notification>();

            CreateMap<Notification, NotificationResponseDto>();

            CreateMap<NotificationUpdateDto, Notification>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
    
}
