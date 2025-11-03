using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities
{
    public class UserOtp
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string OtpCode { get; set; }
        public DateTime OtpExpiry { get; set; }
    }
}
