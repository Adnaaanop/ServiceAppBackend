using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Payment
{
    public class ReviewCreateDto
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProviderId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }

    public class ReviewUpdateDto
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }

    public class ReviewResponseDto
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProviderId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}
