using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared;
using PortalRentCar.Services.Implementaciones;

namespace PortalRentCar.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginDtoRequest request)
        {
            var response = await _usuarioService.LoginAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(RegistrarUsuarioDto request)
        {
            var response = await _usuarioService.RegisterAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("sendTokenToResetPassword")]
        public async Task<IActionResult> sendTokenToResetPassword(GenerateTokenToResetDtoRequest request)
        {
            var response = await _usuarioService.SendTokenToResetPasswordAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> resetPassword(ResetPasswordDtoRequest request)
        {
            var response = await _usuarioService.ResetPasswordAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

    }
}
