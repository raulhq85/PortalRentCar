using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalRentCar.Entities;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Shared;
using PortalRentCar.Shared.Request;

namespace PortalRentCar.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoVehiculoController : ControllerBase
    {
        private readonly ITipoVehiculoService _tipoVehiculoService;

        public TipoVehiculoController(ITipoVehiculoService tipoVehiculoService)
        {
            _tipoVehiculoService = tipoVehiculoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _tipoVehiculoService.ListAsync();

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Post(TipoVehiculoDtoRequest request)
        {
            var response = await _tipoVehiculoService.AddAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Put(int id, TipoVehiculoDtoRequest request)
        {
            var response = await _tipoVehiculoService.UpdateAsync(id, request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _tipoVehiculoService.DeleteAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _tipoVehiculoService.FindByIdAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

    }
}
