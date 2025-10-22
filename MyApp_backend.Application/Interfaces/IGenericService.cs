using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Interfaces
{
    public interface IGenericService<TCreateDto, TUpdateDto, TResponseDto, TEntity>
    {
        Task<TResponseDto> CreateAsync(TCreateDto dto);
        Task<TResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<TResponseDto>> GetAllAsync();
        Task<TResponseDto?> UpdateAsync(Guid id, TUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
