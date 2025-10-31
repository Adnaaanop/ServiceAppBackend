using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Payment
{
    public class PaymentOrderCreateRequestDto
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
    }
}
