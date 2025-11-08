using MyApp_backend.Application.DTOs.Waranty;
using MyApp_backend.Domain.Entities.WarrantySafety;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Interfaces
{
    public interface IWarrantyRequestService // No need to extend generic here if custom
    {
        Task<WarrantyRequestResponseDto> CreateAsync(WarrantyRequestCreateDto dto);
        Task<WarrantyRequestResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<WarrantyRequestResponseDto>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<WarrantyRequestResponseDto>> GetByStatusAsync(WarrantyStatus status);
        Task<WarrantyRequestResponseDto> UpdateStatusAsync(Guid id, WarrantyRequestUpdateDto dto);
    }
}
