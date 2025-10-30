using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities.Payment
{
    public class Review : BaseEntity
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProviderId { get; set; }
        public int Rating { get; set; } // 1 to 5
        public string? Comment { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
