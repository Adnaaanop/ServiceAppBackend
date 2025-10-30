using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities.Payment
{
    public class Payment : BaseEntity
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!; // e.g., Paid, Pending, Refunded
        public string? TransactionId { get; set; }
        public string? InvoiceUrl { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
