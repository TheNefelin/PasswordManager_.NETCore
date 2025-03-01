using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;
using ClassLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/core")]
    [ApiController]
    public class CoreController : ControllerBase
    {
        private readonly ICoreService _coreService;

        public CoreController(ICoreService coreService)
        {
            _coreService = coreService;
        }

        [HttpPatch("register")]
        public async Task<ActionResult<ResponseApi<CoreIVDTO>>> Register(CoreRequest<object> request, CancellationToken cancellationToken)
        {
            var response = await _coreService.RegisterAsync(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("login")]
        public async Task<ActionResult<ResponseApi<CoreIVDTO>>> GetIV(CoreRequest<object> request, CancellationToken cancellationToken)
        {
            var response = await _coreService.GetIVAsync(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("get-all")]
        public async Task<ActionResult<ResponseApi<IEnumerable<CoreDTO>>>> GetAll(CoreRequest<object> request, CancellationToken cancellationToken)
        {
            var response = await _coreService.GetAllAsync(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("insert")]
        public async Task<ActionResult<ResponseApi<CoreDTO>>> Insert(CoreRequest<CoreDTO> request, CancellationToken cancellationToken)
        {
            var response = await _coreService.InsertAsync(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("update")]
        public async Task<ActionResult<ResponseApi<CoreDTO>>> Update(CoreRequest<CoreDTO> request, CancellationToken cancellationToken)
        {
            var response = await _coreService.UpdateAsync(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("delete")]
        public async Task<ActionResult<ResponseApi<object>>> Delete(CoreRequest<CoreDTO> request, CancellationToken cancellationToken)
        {
            var response = await _coreService.DeleteAsync(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
