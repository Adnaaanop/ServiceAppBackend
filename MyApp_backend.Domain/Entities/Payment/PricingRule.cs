using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities.Payment
{
    public class PricingRule : BaseEntity
    {
        public Guid? ServiceId { get; set; }
        public Guid? ProviderId { get; set; }
        public decimal? CommissionRate { get; set; } // e.g., 10.00 for 10%
        public decimal? BasePrice { get; set; }
    }
}
