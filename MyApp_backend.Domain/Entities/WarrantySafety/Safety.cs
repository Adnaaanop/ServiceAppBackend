using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities.WarrantySafety
{
    public class EmergencyAlert
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProviderId { get; set; }
        public DateTime AlertTime { get; set; }
        public string Description { get; set; }
        public bool IsHandled { get; set; }
    }

    public class SafetyTip
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
