using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using PortalRentCar.DataAcces;
using PortalRentCar.Entities;
using PortalRentCar.Repositories.Inplementaciones;
using PortalRentCar.Repositories.Interfaces;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Services.Utils;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PortalRentCar.Services.Implementaciones
{
    public class AlquilerService : IAlquilerService
    {
        //private readonly IVentaDetalleRepository _repository;
        private readonly IAlquilerRepository _alquilerRepository;
        private readonly IVehiculoService _vehiculoService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUbicacionVehiculoRepository _ubicacionVehiculoRepository;
        private readonly ILogger<AlquilerService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<RentCarIdentityUser> _userManager;

        public AlquilerService(UserManager<RentCarIdentityUser> userManager,
                               IAlquilerRepository alquilerRepository, 
                               IVehiculoService vehiculoService, 
                               IClienteRepository clienteRepository, 
                               IUbicacionVehiculoRepository ubicacionVehiculoRepository,
                               ILogger<AlquilerService> logger, IMapper mapper)
        {
            _alquilerRepository = alquilerRepository;
            _clienteRepository = clienteRepository;
            _vehiculoService = vehiculoService;
            _ubicacionVehiculoRepository = ubicacionVehiculoRepository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task<BaseResponse> AddAsync(string usuarioMail, AlquilerDtoRequest request)
        {

            var response = new BaseResponse();

            try
            {

                var lstVentadetalle = new List<Alquiler>();

                var cliente = await _clienteRepository.GetClienteByEmailAsync( (usuarioMail == null? "GRAULEX@GMAIL.COM":usuarioMail));
                if (cliente is null)
                {
                    response.ErrorMessage = "El cliente no existe";
                    return response;
                }

                var ultimoAlquiler = await _alquilerRepository.GetUltimoAlquilerAsync();

                string nroAlquiler = GenerateNroAlquiler(ultimoAlquiler?.NroAlquiler);

                var alquiler = new Alquiler
                {
                    NroAlquiler = nroAlquiler,
                    ClienteId = cliente.Id,
                    PrecioDia = request.PrecioDia,
                    FechaInicio = request.FechaInicio,
                    FechaFin = request.FechaFin,
                    PrecioTotal = request.PrecioTotal,
                    VehiculoId = request.VehiculoId
                };

                var entity = _mapper.Map<Alquiler>(alquiler);

                //grabamos el alquiler
                await _alquilerRepository.AddAsync(entity);
                int idAlquilerGenerado = alquiler.Id;

                // actualizamos el estado del vehiculo
                var vehiculo = await _vehiculoService.FindByIdAsync(request.VehiculoId);

                vehiculo.Data.SituacionVehiculo = 1;

                await _vehiculoService.UpdateAsync(request.VehiculoId, vehiculo.Data);

                // insertamos en la tabla de georecorrido
                var randomGeorrecorrido = GetRandomGeorecorrido();

                var ubicacion = new UbicacionVehiculo
                {
                    Latitud = randomGeorrecorrido.Latitude,
                    Longitud = randomGeorrecorrido.Longitude,
                    FechaPosteo = DateTime.UtcNow,
                    VehiculoId = request.VehiculoId
                };

                await _ubicacionVehiculoRepository.AddAsync(ubicacion);

                response.ErrorMessage = "Su alquiler se arealizado con exito con el Nro: " + nroAlquiler;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al agregar la venta";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            //try
            //{

            //    var entityDet = await _repository.FindByIdAsync(id);

            //    if (entityDet == null)
            //    {
            //        response.ErrorMessage = "No se encontró el detalle de la venta";
            //        return response;
            //    }

            //    var registro = await _repositoryProducto.FindByIdAsync(entityDet.ProductoId);

            //    ProductoDtoRequest prod = new ProductoDtoRequest();

            //    prod.Cantidad = registro.Cantidad + entityDet.Cantidad;
            //    prod.Nombre = registro.Nombre;
            //    prod.Descripcion = registro.Descripcion;
            //    prod.Id = registro.Id;
            //    prod.PrecioCompra = registro.PrecioCompra;
            //    prod.PrecioVenta = registro.PrecioVenta;
            //    prod.CategoriaId = registro.CategoriaId;
            //    prod.Url = registro.Url;

            //    //producto
            //    await _productoService.UpdateAsync(registro.Id, prod);
            //    //detalle venta
            //    await _repository.DeleteAsync(entityDet.Id);
            //    //venta
            //    await _repositoryVenta.DeleteAsync(entityDet.VentaId);

            //    response.Success = true;

            //}
            //catch (Exception ex)
            //{
            //    response.ErrorMessage = "Error al eliminar la venta";
            //    _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            //}
            return response;
        }

        public async Task<PaginationResponse<AlquilerDtoResponse>> ListAsync(AlquilerSearchRequest request, string usuarioMail)
        {
            var response = new PaginationResponse<AlquilerDtoResponse>();
            try
            {

                RentCarIdentityUser? usuario = null;
                usuario = await _userManager.FindByEmailAsync((usuarioMail == null ? "GRAULEX@GMAIL.COM" : usuarioMail));
                var roles = await _userManager.GetRolesAsync(usuario);
                var isAdmin = roles.Contains("Administrador");

                if (!isAdmin)
                {
                    var clienteUser = await _clienteRepository.GetClienteByEmailAsync((usuarioMail == null ? "GRAULEX@GMAIL.COM" : usuarioMail));
                    if (clienteUser is null)
                    {
                        response.ErrorMessage = "El cliente no existe";
                        return response;
                    }

                    request.ClienteId = clienteUser.Id;
                }
                else
                {
                    request.ClienteId = null;
                }
                var tupla = await _alquilerRepository.ListarAlquileresAsync(request.ClienteId, request.Placa, request.TipoVehiculoId, request.MarcaId,request.PrecioMinimo, request.PrecioMaximo, request.Pagina, request.Filas);
                response.Data = _mapper.Map<ICollection<AlquilerDtoResponse>>(tupla.Collection);
                response.TotalPages = Helper.GetTotalPages(tupla.Total, request.Filas);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los alquileres";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        private string GenerateNroAlquiler(string? ultimoNroAlquiler)
        {
            if (string.IsNullOrEmpty(ultimoNroAlquiler))
            {
                return "A" + DateTime.UtcNow.Year + "-000001";
            }

            string correlativoActualStr = ultimoNroAlquiler.Substring(ultimoNroAlquiler.Length - 6);
            if (int.TryParse(correlativoActualStr, out int correlativoActual))
            {
                int siguienteCorrelativo = correlativoActual + 1;
                return $"A{DateTime.UtcNow.Year}-{siguienteCorrelativo:D6}";
            }
            else
            {
                throw new InvalidOperationException("Formato incorrecto.");
            }
        }

        public static (double Latitude, double Longitude) GetRandomGeorecorrido()
        {
            double centerLat = -12.172202;
            double centerLon = -76.959292;
            double radius = 5000;

            const double EarthRadius = 6378137; // Radio
            double radiusInDegrees = radius / EarthRadius * (180 / Math.PI);

            Random rand = new Random();
            double randomAngle = rand.NextDouble() * 2 * Math.PI;
            double randomDistance = rand.NextDouble() * radiusInDegrees;

            double deltaLat = randomDistance * Math.Cos(randomAngle);
            double deltaLon = randomDistance * Math.Sin(randomAngle) / Math.Cos(centerLat * Math.PI / 180);

            double newLat = centerLat + deltaLat;
            double newLon = centerLon + deltaLon;

            return (newLat, newLon);
        }

    }
}
