using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities
{
    public class Message : BaseEntity
    {
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }

        public Guid SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public Guid ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        public string MessageText { get; set; }
        public string? MediaUrl { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
