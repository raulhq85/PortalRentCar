using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Shared;
using PortalRentCar.Shared.Request;

namespace PortalRentCar.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ClienteSearchDtoRequest request)
        {
            var response = await _service.ListAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        //[Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Put(int id, ClienteDtoRequest request)
        {
            var response = await _service.UpdateAsync(id, request);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
