using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;

namespace MauiAppAdmin.Services
{
    public interface IApiAuthService
    {
        Task<ResponseApi<UserIdDTO>> RegisterAsync(AuthRegister authRegister);
        Task<ResponseApi<AuthLogged>> LoginAsync(AuthLogin authLogin);
    }
}
