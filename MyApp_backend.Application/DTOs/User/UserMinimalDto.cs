using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.User
{
    public class UserMinimalDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public string Role { get; set; } = "User";
        public bool IsVerified { get; set; }
    }
}
