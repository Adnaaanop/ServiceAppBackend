using MyApp_backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string? Name { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public bool IsVerified { get; set; } = false;

        // Address stored as JSON string (structured as street, city, zip as per your DB)
        public string? AddressJson { get; set; }

        // Preferred Services stored as JSON array of category IDs
        public string? PreferredServicesJson { get; set; }

        // Authentication-related fields
        

        // Soft delete and auditing handled by BaseEntity:
        // Id, CreatedAt, LastUpdatedAt, IsDeleted, IsActive, DeletedAt, LastUpdatedBy
    }
}
