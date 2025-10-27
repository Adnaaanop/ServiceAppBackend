using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyApp_backend.Domain.Entities
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

        //--------------------------
        //public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime? DeletedAt { get; set; }
        public Guid? LastUpdatedBy { get; set; }
    }
}
