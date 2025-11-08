using MyApp_backend.Application.DTOs.Waranty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Interfaces
{
    public interface IEmergencyAlertService
    {
        Task<EmergencyAlertResponseDto> CreateAsync(EmergencyAlertCreateDto dto);
        Task<IEnumerable<EmergencyAlertResponseDto>> GetActiveAlertsAsync();
        Task<EmergencyAlertResponseDto> GetByIdAsync(Guid id);
        Task<EmergencyAlertResponseDto> MarkAsHandledAsync(Guid id);
    }
}
