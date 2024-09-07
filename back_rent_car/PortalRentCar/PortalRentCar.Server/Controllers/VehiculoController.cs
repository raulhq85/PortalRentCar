using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Shared;
using PortalRentCar.Shared.Request;

namespace PortalRentCar.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculoController : Controller
    {
        private readonly IVehiculoService _vehiculoService;
        private readonly IUbicacionService _IUbicacionService;

        public VehiculoController(IVehiculoService vehiculoService, IUbicacionService iUbicacionService)
        {
            _vehiculoService = vehiculoService;
            _IUbicacionService = iUbicacionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] VehiculoSearchRequest request)
        {
            var response = await _vehiculoService.ListAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Post(VehiculoDtoRequest request)
        {
            var response = await _vehiculoService.AddAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Put(int id, VehiculoDtoRequest request)
        {
            var response = await _vehiculoService.UpdateAsync(id, request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _vehiculoService.DeleteAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _vehiculoService.FindByIdAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetVehiculoHome")]
        public async Task<IActionResult> GetVehiculoHome([FromQuery] VehiculoSearchHomeRequest request)
        {

            request.Pagina =  request.Pagina <= 0 ?1:request.Pagina;
            request.Filas = request.Filas <= 0 ?5:request.Filas;

            var response = await _vehiculoService.ListarVehiculosHomeAsync(request);
            return Ok(response);
        }

        [HttpGet("GetVehiculoHomeById")]
        public async Task<IActionResult> GetVehiculoHomeById(int id)
        {
            var response = await _vehiculoService.GetVehiculoHomeAsyncById(id);
            return Ok(response);
        }

        [HttpGet("GetListVehiculoUbicacion")]
        public async Task<IActionResult> GetListVehiculoUbicacion()
        {
            var response = await _IUbicacionService.GetListUbicacionVehiculo();

            return response.Success ? Ok(response) : BadRequest(response);
        }

    }
}
