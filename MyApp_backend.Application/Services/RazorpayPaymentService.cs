using AutoMapper;
using Microsoft.Extensions.Options;
using MyApp_backend.Application.DTOs.Payment;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Application.Settings;
using MyApp_backend.Domain.Entities.Payment;
using MyApp_backend.Domain.Interfaces;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using ProjectPayment = MyApp_backend.Domain.Entities.Payment.Payment;
namespace MyApp_backend.Application.Services
{
    public class RazorpayPaymentService : IPaymentService
    {
        private readonly IGenericRepository<ProjectPayment> _repository;
        private readonly IMapper _mapper;
        private readonly RazorpayClient _client;
        private readonly RazorpaySettings _razorpaySettings;

        public RazorpayPaymentService(
            IGenericRepository<ProjectPayment> repository,
            IMapper mapper,
            IOptions<RazorpaySettings> options)
        {
            _repository = repository;
            _mapper = mapper;
            _razorpaySettings = options.Value;
            _client = new RazorpayClient(_razorpaySettings.KeyId, _razorpaySettings.KeySecret);
        }

        public async Task<string> CreateRazorpayOrderAsync(decimal amount, Guid bookingId)
        {
            var options = new Dictionary<string, object>
            {
                { "amount", (int)(amount * 100) }, // paise
                { "currency", "INR" },
                { "receipt", bookingId.ToString() },
                { "payment_capture", 1 }
            };

            var order = _client.Order.Create(options);

            var payment = new ProjectPayment
            {
                BookingId = bookingId,
                Amount = amount,
                Status = "created",
                TransactionId = order["id"].ToString(),
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(payment);
            return order["id"].ToString();
        }

        // HMAC SHA256 manual signature verification, as Razorpay SDK does not provide this utility in C#
        public bool VerifyPaymentSignature(string orderId, string paymentId, string signature)
        {
            string payload = $"{orderId}|{paymentId}";

            var secretBytes = Encoding.UTF8.GetBytes(_razorpaySettings.KeySecret);
            var payloadBytes = Encoding.UTF8.GetBytes(payload);

            using (var hmac = new HMACSHA256(secretBytes))
            {
                var hash = hmac.ComputeHash(payloadBytes);
                var generatedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return generatedSignature == signature.ToLower();
            }
        }

        public async Task<PaymentResponseDto> CreateAsync(PaymentCreateDto dto)
        {
            var entity = _mapper.Map<ProjectPayment>(dto);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<PaymentResponseDto>(created);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PaymentResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<PaymentResponseDto>>(entities);
        }

        public async Task<PaymentResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<PaymentResponseDto>(entity);
        }

        public async Task<IEnumerable<PaymentResponseDto>> GetByBookingIdAsync(Guid bookingId)
        {
            var entities = await _repository.FindAsync(x => x.BookingId == bookingId && !x.IsDeleted);
            return _mapper.Map<List<PaymentResponseDto>>(entities);
        }

        public async Task<PaymentResponseDto?> UpdateAsync(Guid id, PaymentUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<PaymentResponseDto>(entity);
        }
    }
}
