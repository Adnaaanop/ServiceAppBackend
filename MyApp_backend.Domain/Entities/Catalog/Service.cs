using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities.Catalog
{
    public class Service : BaseEntity
    {
        public Guid ProviderId { get; set; }
        public ApplicationUser Provider { get; set; }

        public Guid CategoryId { get; set; }
        public ServiceCategory Category { get; set; }

        public string? Description { get; set; }

        // Store pricing, e.g. JSON: {"min":50,"max":150,"formula":"price * hours"}
        public string PricingJson { get; set; }

        // Provider sets an availability schedule, e.g. JSON or cron-style
        public string? AvailabilityScheduleJson { get; set; }

        // Media URLs (photos/video of work, before/after, etc.), as JSON array
        public string? MediaUrlsJson { get; set; }

        // Admin-controlled approval status
        public bool IsApproved { get; set; } = false;

        // Statistics
        public int TotalBookings { get; set; } = 0;
        public double AverageRating { get; set; } = 0.0;

        // Cost estimation field (optional, for UI instant quote)
        public decimal? EstimatedCost { get; set; }
    }

}
