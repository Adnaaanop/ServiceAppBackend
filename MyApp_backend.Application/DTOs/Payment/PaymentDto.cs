using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Payment
{
    public class PaymentCreateDto
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
        public string? TransactionId { get; set; }
        public string? InvoiceUrl { get; set; }
        public string? PaymentMethod { get; set; }

        // Razorpay specific
        public string? RazorpayOrderId { get; set; }
        public string? RazorpayPaymentId { get; set; }
        public string? RazorpaySignature { get; set; }
    }

    public class PaymentUpdateDto
    {
        public string Status { get; set; } = null!;
        public string? TransactionId { get; set; }
        public string? InvoiceUrl { get; set; }

        public string? RazorpayPaymentId { get; set; }
        public string? RazorpaySignature { get; set; }
    }

    public class PaymentResponseDto
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
        public string? TransactionId { get; set; }
        public string? InvoiceUrl { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

        // Razorpay specific
        public string? RazorpayOrderId { get; set; }
        public string? RazorpayPaymentId { get; set; }
        public string? RazorpaySignature { get; set; }
    }
}
