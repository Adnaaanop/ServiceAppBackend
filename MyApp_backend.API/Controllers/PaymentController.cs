using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Payment;
using MyApp_backend.Application.Interfaces;

namespace MyApp_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentResponseDto>>> GetAll()
        {
            var payments = await _paymentService.GetAllAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentResponseDto>> GetById(Guid id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<List<PaymentResponseDto>>> GetByBookingId(Guid bookingId)
        {
            var payments = await _paymentService.GetByBookingIdAsync(bookingId);
            return Ok(payments);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentResponseDto>> Create(PaymentCreateDto dto)
        {
            var payment = await _paymentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentResponseDto>> Update(Guid id, PaymentUpdateDto dto)
        {
            var updatedPayment = await _paymentService.UpdateAsync(id, dto);
            if (updatedPayment == null) return NotFound();
            return Ok(updatedPayment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _paymentService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
