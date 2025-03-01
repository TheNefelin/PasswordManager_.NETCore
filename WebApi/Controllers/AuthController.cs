using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;
using ClassLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [ServiceFilter(typeof(ApiKeyFilter))]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ResponseApi<UserIdDTO>>> Register(AuthRegister registerDTO, CancellationToken cancellationToken)
        {
            var response = await _authService.RegisterAsync(registerDTO, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ResponseApi<AuthLogged>>> Login(AuthLogin loginDTO, CancellationToken cancellationToken)
        {
            var jwtConfig = new JwtConfig()
            {
                Key = _configuration["Jwt:Key"]!,
                Issuer = _configuration["Jwt:Issuer"]!,
                Audience = _configuration["Jwt:Audience"]!,
                Subject = _configuration["JWT:Subject"]!,
                ExpireMin = _configuration["JWT:ExpireMin"]!
            };

            var response = await _authService.LoginAsync(loginDTO, jwtConfig, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
