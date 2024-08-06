using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Shared;
using PortalRentCar.Shared.Request;
using System.Security.Claims;

namespace PortalRentCar.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlquilerController : Controller
    {
        private IAlquilerService _alquilerService;
        private readonly ILogger<AlquilerController> _logger;

        public AlquilerController(IAlquilerService alquilerService, ILogger<AlquilerController> logger)
        {
            _alquilerService = alquilerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AlquilerSearchRequest request)
        {
            var cliente = User.Claims.First(p => p.Type == ClaimTypes.Email).Value;

            var response = await _alquilerService.ListAsync(request, cliente);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        //[Authorize(Roles = Constantes.RolCliente)]
        public async Task<IActionResult> Post([FromBody] AlquilerDtoRequest request)
        {

            var usuario = User.Claims.First(p => p.Type == ClaimTypes.Email).Value;

            _logger.LogInformation(usuario);

            var response = await _alquilerService.AddAsync(usuario, request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        //[Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _alquilerService.DeleteAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

    }
}
