using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;

namespace ClassLibrary.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseApi<object>> RegisterAsync(AuthRegister register, CancellationToken cancellationToken);
        Task<ResponseApi<AuthLogged>> LoginAsync(AuthLogin login, JwtConfig jwtConfig, CancellationToken cancellationToken);
    }
}
