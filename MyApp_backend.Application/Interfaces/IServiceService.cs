using MyApp_backend.Application.DTOs.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Interfaces
{
    public interface IServiceService
         : IGenericService<ServiceCreateDto, ServiceUpdateDto, ServiceResponseDto, Domain.Entities.Catalog.Service>
    {
        Task<IEnumerable<ServiceResponseDto>> GetByProviderIdAsync(Guid providerId);
    }
}
