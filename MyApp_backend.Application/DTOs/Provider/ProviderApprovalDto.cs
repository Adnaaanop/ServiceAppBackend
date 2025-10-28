using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Provider
{
    public class ProviderApprovalDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public bool IsApproved { get; set; }
    }
}
