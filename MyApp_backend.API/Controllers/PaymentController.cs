using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp_backend.Application.DTOs.Payment;
using MyApp_backend.Application.Interfaces;
using Razorpay.Api;

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





        // Create Razorpay order
        [HttpPost("create-order")]
        public async Task<ActionResult<string>> CreateOrder([FromBody] PaymentOrderCreateRequestDto dto)
        {
            if (dto == null || dto.Amount <= 0 || dto.BookingId == Guid.Empty)
                return BadRequest("Invalid input");
            try
            {
                var orderId = await _paymentService.CreateRazorpayOrderAsync(dto.Amount, dto.BookingId);
                return Ok(new { RazorpayOrderId = orderId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating Razorpay order: " + ex.Message);
            }
        }

        [HttpPost("verify-payment")]
        public async Task<IActionResult> VerifyPayment([FromBody] PaymentVerificationDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.RazorpayOrderId)
                || string.IsNullOrEmpty(dto.RazorpayPaymentId)
                || string.IsNullOrEmpty(dto.RazorpaySignature))
                return BadRequest("Invalid data");

            try
            {
                var isValid = _paymentService.VerifyPaymentSignature(dto.RazorpayOrderId, dto.RazorpayPaymentId, dto.RazorpaySignature);
                if (!isValid)
                    return BadRequest("Invalid payment signature");

                // Update payment status to captured/successful
                var paymentToUpdate = await _paymentService.GetByRazorpayOrderIdAsync(dto.RazorpayOrderId);


                if (paymentToUpdate != null)
                {
                    var updateDto = new PaymentUpdateDto
                    {
                        Status = "Paid",
                        TransactionId = dto.RazorpayPaymentId
                        // Add other fields if needed
                    };
                    await _paymentService.UpdateAsync(paymentToUpdate.Id, updateDto);
                }

                return Ok(new { Message = "Payment verified successfully" });
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "Error verifying payment: " + ex.Message);
            }
        }



    }
}
