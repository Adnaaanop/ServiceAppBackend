using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Provider
{
    public class ProviderRegisterDto
    {
        // Step 1: Personal & Business Info
        [Required]
        public string FullName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, Phone]
        public string PhoneNumber { get; set; } = null!;

        [Required, MinLength(6)]
        public string Password { get; set; } = null!;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        public string BusinessName { get; set; } = null!;

        public string? BusinessDescription { get; set; }

        // Step 2: Service Categories
        [Required]
        public List<string> ServiceCategories { get; set; } = new();

        // Step 3: Credentials/Certificates/Docs (upload links instead of uploads)
        public List<string>? CertificateUrls { get; set; }
        public List<string>? LicenseUrls { get; set; }
        public List<string>? DocumentUrls { get; set; }

        // Step 4: Service Areas
        public List<string>? ServiceAreas { get; set; }

        // Step 5: Availability Schedule (as JSON string or custom object)
        public string? AvailabilityJson { get; set; }
    }
}
