using MyApp_backend.Domain.Entities.WarrantySafety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Interfaces
{
    public interface IEmergencyAlertRepository : IGenericRepository<EmergencyAlert>
    {
        Task<IEnumerable<EmergencyAlert>> GetActiveAlertsAsync();
    }
}
