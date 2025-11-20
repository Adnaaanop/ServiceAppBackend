using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Message
{
    // MessageCreateDto.cs
    public class MessageCreateDto
    {
        public Guid BookingId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string MessageText { get; set; }
        public IFormFile? MediaFile { get; set; }
    }

    // MessageResponseDto.cs
    public class MessageResponseDto
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string MessageText { get; set; }
        public string? MediaUrl { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
