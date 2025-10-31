using AutoMapper;
using MyApp_backend.Application.DTOs.Payment;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Domain.Entities.Payment;
using MyApp_backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IGenericRepository<Payment> _repository;
        private readonly IMapper _mapper;

        public PaymentService(IGenericRepository<Payment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaymentResponseDto> CreateAsync(PaymentCreateDto dto)
        {
            var entity = _mapper.Map<Payment>(dto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<PaymentResponseDto>(createdEntity);
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
        public Task<string> CreateRazorpayOrderAsync(decimal amount, Guid bookingId)
        {
            throw new NotImplementedException("Razorpay order creation is not supported in standard PaymentService.");
        }

        public bool VerifyPaymentSignature(string orderId, string paymentId, string signature)
        {
            throw new NotImplementedException("Razorpay signature verification is not supported in standard PaymentService.");
        }
        public async Task<PaymentResponseDto?> GetByRazorpayOrderIdAsync(string razorpayOrderId)
        {
            var payments = await _repository.FindAsync(x => x.RazorpayOrderId == razorpayOrderId && !x.IsDeleted);
            var entity = payments.FirstOrDefault();
            return entity == null ? null : _mapper.Map<PaymentResponseDto>(entity);
        }
    }
}
