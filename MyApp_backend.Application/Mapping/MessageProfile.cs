using AutoMapper;
using MyApp_backend.Application.DTOs.Message;
using MyApp_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Mapping
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageCreateDto, Message>();
            CreateMap<Message, MessageResponseDto>();
        }
    }
}
