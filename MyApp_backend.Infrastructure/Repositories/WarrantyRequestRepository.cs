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
    public class WarrantyRequestRepository : GenericRepository<WarrantyRequest>, IWarrantyRequestRepository
    {
        public WarrantyRequestRepository(MyAppDbContext context) : base(context) { }

        public async Task<IEnumerable<WarrantyRequest>> GetByUserIdAsync(Guid userId)
        {
            return await FindAsync(w => w.UserId == userId);
        }

        public async Task<IEnumerable<WarrantyRequest>> GetByStatusAsync(WarrantyStatus status)
        {
            return await FindAsync(w => w.Status == status);
        }
    }
}
