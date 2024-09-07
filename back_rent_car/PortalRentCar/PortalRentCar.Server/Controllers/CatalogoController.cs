using Microsoft.AspNetCore.Mvc;
using PortalRentCar.Services.Implementaciones;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Shared.Request;

namespace VentaGalaxy.WebService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogoController : ControllerBase
{
    private readonly IVehiculoService _vehiculoService;

    public CatalogoController(IVehiculoService vehiculoService)
    {
        _vehiculoService = vehiculoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] VehiculoSearchHomeRequest request)
    {
        if (request.Pagina <= 0)
            request.Pagina = 1;
        if (request.Filas <= 0)
            request.Filas = 5;

        var response = await _vehiculoService.ListarVehiculosHomeAsync(request);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _vehiculoService.GetVehiculoHomeAsyncById(id);
        return Ok(response);
    }
}