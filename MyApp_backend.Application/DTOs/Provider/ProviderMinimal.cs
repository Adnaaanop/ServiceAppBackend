using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Provider
{
    public class ProviderMinimalDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string VerificationStatus { get; set; } = "Pending";
    }
}
