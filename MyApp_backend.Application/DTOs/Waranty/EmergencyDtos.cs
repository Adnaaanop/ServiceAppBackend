using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Waranty
{
    // EmergencyAlertCreateDto.cs
    public class EmergencyAlertCreateDto
    {
        public Guid UserId { get; set; }
        public Guid ProviderId { get; set; }
        public string Description { get; set; }
    }

    // EmergencyAlertResponseDto.cs
    public class EmergencyAlertResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProviderId { get; set; }
        public DateTime AlertTime { get; set; }
        public string Description { get; set; }
        public bool IsHandled { get; set; }
    }
}
