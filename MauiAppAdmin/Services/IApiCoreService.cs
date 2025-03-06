using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;

namespace MauiAppAdmin.Services
{
    public interface IApiCoreService
    {
        Task<ResponseApi<CoreIVDTO>> GetIVAsync(CoreRequest<object> coreRequest, string apiKeyToken);
        Task<ResponseApi<IEnumerable<CoreDTO>>> GetAllAsync(CoreRequest<object> coreRequest, string apiKeyToken);
    }
}
