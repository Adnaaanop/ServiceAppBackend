using MyApp_backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ProviderId { get; set; }
        public Guid ServiceId { get; set; }

        public DateTime TimeSlotStart { get; set; }
        public DateTime? TimeSlotEnd { get; set; }

        //public string Status { get; set; } // e.g., Pending, Accepted, Completed, Cancelled

        public string Location { get; set; } // JSON string

        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }

        public BookingStatus Status { get; set; }
        // Add this navigation property:
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}

