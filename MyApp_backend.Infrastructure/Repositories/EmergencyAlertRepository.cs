using MyApp_backend.Domain.Entities.WarrantySafety;
using MyApp_backend.Domain.Interfaces;
using MyApp_backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Infrastructure.Repositories
{
    public class EmergencyAlertRepository : GenericRepository<EmergencyAlert>, IEmergencyAlertRepository
    {
        public EmergencyAlertRepository(MyAppDbContext context) : base(context) { }

        public async Task<IEnumerable<EmergencyAlert>> GetActiveAlertsAsync()
        {
            return await FindAsync(e => !e.IsHandled);
        }
    }
}
