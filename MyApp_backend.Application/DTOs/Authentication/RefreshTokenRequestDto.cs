using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Authentication
{
    public class RefreshTokenRequestDto
    {
        public string Token { get; set; } = null!;          // Expired JWT token
        public string RefreshToken { get; set; } = null!;   // Stored refresh token
    }
}
