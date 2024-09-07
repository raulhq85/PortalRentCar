using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalRentCar.Services.Implementaciones;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared;

namespace PortalRentCar.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : Controller
    {
        private readonly IMarcaService _marcaService;

        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _marcaService.ListAsync();

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Post(MarcaDtoRequest request)
        {
            var response = await _marcaService.AddAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Put(int id, MarcaDtoRequest request)
        {
            var response = await _marcaService.UpdateAsync(id, request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _marcaService.DeleteAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _marcaService.FindByIdAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
