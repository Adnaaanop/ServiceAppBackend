using MyApp_backend.Application.DTOs.Payment;
using MyApp_backend.Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Interfaces
{
    public interface IPaymentService : IGenericService<PaymentCreateDto, PaymentUpdateDto, PaymentResponseDto, Payment>
    {
        Task<IEnumerable<PaymentResponseDto>> GetByBookingIdAsync(Guid bookingId);

        Task<string> CreateRazorpayOrderAsync(decimal amount, Guid bookingId);
        bool VerifyPaymentSignature(string orderId, string paymentId, string signature);
    }
}
