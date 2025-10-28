using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities
{
    public class ProviderProfile : BaseEntity
    {
        [Key, ForeignKey(nameof(User))]
        public Guid UserId { get; set; }  // PK and FK to ApplicationUser

        [Required]
        public string BusinessName { get; set; } = null!;

        public string? BusinessDescription { get; set; }

        // For simplicity, storing categories, services areas, and file URLs as JSON strings
        public string ServiceCategoriesJson { get; set; } = "[]";

        public string CertificateUrlsJson { get; set; } = "[]";

        public string LicenseUrlsJson { get; set; } = "[]";

        public string DocumentUrlsJson { get; set; } = "[]";

        public string ServiceAreasJson { get; set; } = "[]";

        public string? AvailabilityJson { get; set; }

        public bool IsApproved { get; set; } = false;

        public virtual ApplicationUser User { get; set; } = null!;
    }
}
