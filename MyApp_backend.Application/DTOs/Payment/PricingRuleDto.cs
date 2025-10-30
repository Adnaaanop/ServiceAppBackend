using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Payment
{
    public class PricingRuleCreateDto
    {
        public Guid? ServiceId { get; set; }
        public Guid? ProviderId { get; set; }
        public decimal? CommissionRate { get; set; }
        public decimal? BasePrice { get; set; }
    }

    public class PricingRuleUpdateDto
    {
        public decimal? CommissionRate { get; set; }
        public decimal? BasePrice { get; set; }
    }

    public class PricingRuleResponseDto
    {
        public Guid Id { get; set; }
        public Guid? ServiceId { get; set; }
        public Guid? ProviderId { get; set; }
        public decimal? CommissionRate { get; set; }
        public decimal? BasePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}
