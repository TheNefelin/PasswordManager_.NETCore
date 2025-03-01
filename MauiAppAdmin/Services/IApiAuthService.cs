using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;

namespace MauiAppAdmin.Services
{
    public interface IApiAuthService
    {
        Task<ResponseApi<AuthLogged>> LoginAsync(AuthLogin authLogin);
    }
}
