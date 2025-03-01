using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;

namespace ClassLibrary.Services.Interfaces
{
    public interface ICoreService
    {
        Task<ResponseApi<CoreIVDTO>> RegisterAsync(CoreRequest<object> request, CancellationToken cancellationToken);
        Task<ResponseApi<CoreIVDTO>> GetIVAsync(CoreRequest<object> request, CancellationToken cancellationToken);
        Task<ResponseApi<IEnumerable<CoreDTO>>> GetAllAsync(CoreRequest<object> request, CancellationToken cancellationToken);
        Task<ResponseApi<CoreDTO>> InsertAsync(CoreRequest<CoreDTO> request, CancellationToken cancellationToken);
        Task<ResponseApi<CoreDTO>> UpdateAsync(CoreRequest<CoreDTO> request, CancellationToken cancellationToken);
        Task<ResponseApi<object>> DeleteAsync(CoreRequest<CoreDTO> request, CancellationToken cancellationToken);
    }
}
