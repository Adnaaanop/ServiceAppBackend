using MyApp_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Interfaces
{
    public interface IProviderRepository
    {
        Task<ProviderProfile> AddAsync(ProviderProfile entity);
        Task<ProviderProfile?> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<ProviderProfile>> GetAllAsync();
        Task<ProviderProfile?> UpdateAsync(ProviderProfile entity);
        Task<bool> DeleteAsync(Guid userId);
    }
}
