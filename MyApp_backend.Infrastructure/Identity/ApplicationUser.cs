using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyApp_backend.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public bool IsVerified { get; set; } = false;
        public string? AddressJson { get; set; }
        public string? PreferredServicesJson { get; set; }
        public DateTime? LastLogout { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
