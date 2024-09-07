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

                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized("Usuario no autorizado.");
                }

                var emailClaim = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(emailClaim))
                {
                    return Unauthorized("User session has expired or the email claim is missing.");
                }

                var cliente = emailClaim;

                var response = await _alquilerService.ListAsync(request, cliente);

                return response.Success ? Ok(response) : BadRequest(response);

        }

        [HttpPost("GenerateAlquilerAsync")]
        [Authorize(Roles = Constantes.RolCliente)]
        public async Task<IActionResult> GenerateAlquilerAsync([FromBody] AlquilerDtoRequest request)
        {

            var usuario = User.Claims.First(p => p.Type == ClaimTypes.Email).Value;

            _logger.LogInformation(usuario);

            var response = await _alquilerService.AddAsync(usuario, request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _alquilerService.DeleteAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpGet("GetDocumentAlquilerByIdAsync")]
        public async Task<IActionResult> GetDocumentAlquilerByIdAsync(int id)
        {
            var response = await _alquilerService.GetDocumentAlquilerByIdAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        



    }
}
