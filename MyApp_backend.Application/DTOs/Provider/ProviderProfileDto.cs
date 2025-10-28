using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Provider
{
    public class ProviderProfileDto
    {
        public string BusinessName { get; set; }
        public string? BusinessDescription { get; set; }
        public List<string> ServiceCategories { get; set; }
        public List<string>? CertificateUrls { get; set; }
        public List<string>? LicenseUrls { get; set; }
        public List<string>? DocumentUrls { get; set; }
        public List<string>? ServiceAreas { get; set; }
        public string? AvailabilityJson { get; set; }
        public bool IsApproved { get; set; }
    }
}
