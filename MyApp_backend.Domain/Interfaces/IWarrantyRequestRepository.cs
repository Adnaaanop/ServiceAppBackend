using MyApp_backend.Domain.Entities.WarrantySafety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Interfaces
{
    public interface IWarrantyRequestRepository : IGenericRepository<WarrantyRequest>
    {
        Task<IEnumerable<WarrantyRequest>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<WarrantyRequest>> GetByStatusAsync(WarrantyStatus status);
    }
}
