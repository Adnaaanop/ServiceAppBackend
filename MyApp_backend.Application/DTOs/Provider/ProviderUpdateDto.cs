using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Provider
{
    public class ProviderUpdateDto
    {
        public string? Skills { get; set; }
        public string? Certifications { get; set; }
        public string? Portfolio { get; set; }
        public string? ServiceZones { get; set; }
        public string? VerificationStatus { get; set; }
    }
}
