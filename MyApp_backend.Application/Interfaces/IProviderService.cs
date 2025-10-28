using MyApp_backend.Application.DTOs.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Interfaces
{
    public interface IProviderService
    {
        Task<Guid> RegisterAsync(ProviderRegisterDto dto);
        Task<ProviderProfileDto?> GetProfileByUserIdAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Guid userId, ProviderUpdateDto dto);
        Task<bool> ApproveAsync(ProviderApprovalDto dto);
        Task<IEnumerable<ProviderProfileDto>> GetAllProvidersAsync(); // for admin listing/all providers
    }
}
