using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Booking
{
    // Used when creating a booking
    public class BookingCreateDto
    {
        public Guid UserId { get; set; }
        public Guid ProviderId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime TimeSlotStart { get; set; }
        public DateTime? TimeSlotEnd { get; set; }
        public string Location { get; set; } // Store location as JSON
        public decimal? EstimatedCost { get; set; }
    }

    public class BookingUpdateDto
    {
        public DateTime TimeSlotStart { get; set; }
        public DateTime? TimeSlotEnd { get; set; }
        public MyApp_backend.Domain.Enums.BookingStatus Status { get; set; } // Use enum here
        public string Location { get; set; }
        public decimal? ActualCost { get; set; }
    }

    public class BookingResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProviderId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime TimeSlotStart { get; set; }
        public DateTime? TimeSlotEnd { get; set; }
        public string Status { get; set; } // Send string to frontend
        public string Location { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
