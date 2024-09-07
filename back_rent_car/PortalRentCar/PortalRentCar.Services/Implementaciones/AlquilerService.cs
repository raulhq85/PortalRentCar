using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PortalRentCar.DataAcces;
using PortalRentCar.Entities;
using PortalRentCar.Repositories.Inplementaciones;
using PortalRentCar.Repositories.Interfaces;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Services.Utils;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using Spire.Doc;

namespace PortalRentCar.Services.Implementaciones
{
    public class AlquilerService : IAlquilerService
    {
        //private readonly IVentaDetalleRepository _repository;
        private readonly IAlquilerRepository _alquilerRepository;
        private readonly IVehiculoService _vehiculoService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly IUbicacionVehiculoRepository _ubicacionVehiculoRepository;
        private readonly ILogger<AlquilerService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<RentCarIdentityUser> _userManager;

        public AlquilerService(UserManager<RentCarIdentityUser> userManager,
                               IAlquilerRepository alquilerRepository, 
                               IVehiculoService vehiculoService, 
                               IClienteRepository clienteRepository, 
                               IUbicacionVehiculoRepository ubicacionVehiculoRepository,
                               IVehiculoRepository vehiculoRepository,
                               ILogger<AlquilerService> logger, IMapper mapper)
        {
            _alquilerRepository = alquilerRepository;
            _clienteRepository = clienteRepository;
            _vehiculoService = vehiculoService;
            _vehiculoRepository = vehiculoRepository;
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

                var cliente = await _clienteRepository.GetClienteByEmailAsync( usuarioMail);
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

                response.ErrorMessage = "Grabado correctamente, Alquiler Nro: " + nroAlquiler;
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
            try
            {

                var alguiler = await _alquilerRepository.FindByIdAsync(id);

                if (alguiler == null)
                {
                    response.ErrorMessage = "No se encontró el detalle del alquiler";
                    return response;
                }

                var respVehiculo = await _vehiculoRepository.FindByIdAsync(alguiler.VehiculoId);

                VehiculoDtoRequest vehiculo = new VehiculoDtoRequest();

                    vehiculo.SituacionVehiculo = 0; // 0 disponible
                    vehiculo.Id = respVehiculo.Id;
                    vehiculo.TipoVehiculoId = respVehiculo.TipoVehiculoId;
                    vehiculo.MarcaId = respVehiculo.MarcaId;
                    vehiculo.Nombre = respVehiculo.Nombre;
                    vehiculo.Color = respVehiculo.Color;
                    vehiculo.Anio = respVehiculo.Anio;
                    vehiculo.Placa = respVehiculo.Placa;
                    vehiculo.Kilometraje = respVehiculo.Kilometraje;
                    vehiculo.Precio = respVehiculo.Precio;
                    vehiculo.ImagenUrL = respVehiculo.ImagenUrL;

                //vehiculo actualizamos el estado para su alquiler

                await _vehiculoService.UpdateAsync(respVehiculo.Id, vehiculo);

                //anulamos el alquiler

                await _alquilerRepository.DeleteAsync(alguiler.Id);

                // quitamos de la ubicacion
                //var ubicacion = await _ubicacionVehiculoRepository.GetAsyncUbicacionByIdVehicleAsync(alguiler.VehiculoId);
                //await _ubicacionVehiculoRepository.DeleteAsync(ubicacion.Id);

                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al eliminar el alquiler";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<PaginationResponse<AlquilerDtoResponse>> ListAsync(AlquilerSearchRequest request, string usuarioMail)
        {
            var response = new PaginationResponse<AlquilerDtoResponse>();
            try
            {

                RentCarIdentityUser? usuario = null;
                usuario = await _userManager.FindByEmailAsync(usuarioMail);
                var roles = await _userManager.GetRolesAsync(usuario);
                var isAdmin = roles.Contains("Administrador");
                int? idUsuario = null;

                if (!isAdmin)
                {
                    var clienteUser = await _clienteRepository.GetClienteByEmailAsync(usuarioMail);
                    if (clienteUser is null)
                    {
                        response.ErrorMessage = "El cliente no existe";
                        return response;
                    }

                    idUsuario = clienteUser.Id;
                }
                else
                {
                    idUsuario = null;
                }

                var tupla = await _alquilerRepository.ListarAlquileresAsync(request.NroAlquiler, request.Vehiculo, idUsuario, request.Placa, request.TipoVehiculoId, request.MarcaId,request.PrecioMinimo, request.PrecioMaximo, request.Pagina, request.Filas);
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
            // geolocalizacion aleatoria para lima
            double centerLat = -12.061059;
            double centerLon = -77.005312;
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

        public async Task<BaseResponse> GetDocumentAlquilerByIdAsync(int id)
        {

            BaseResponse result = new BaseResponse();
            string templatePath = "Templates\\plantilla_alquiler.docx";

            var alquilerResponse = await _alquilerRepository.GetDocumentAlquilerById(id);

            if (!File.Exists(templatePath))
            {
                result.Success = false;
                result.ErrorMessage = "No se tiene plantilla";
                return result;
            }

            try
            {
                Document doc = new Document();
                doc.LoadFromFile(templatePath, FileFormat.Auto);
                CultureInfo culture = new CultureInfo("en-US");

                doc.Replace("[CLIENTE]", alquilerResponse.Cliente,true, true);
                doc.Replace("[NRO_ALQUILER]", alquilerResponse.NroAlquiler, true, true);
                doc.Replace("[FECHA_ALQUILER]", alquilerResponse.Fecha.ToString("dd") + " de " + alquilerResponse.Fecha.ToString("MMMM") + " del " + alquilerResponse.Fecha.ToString("yyyy"), true, true);
                doc.Replace("[NRO_ITEM]", "1", true, true);
                doc.Replace("[VEHICULO]", alquilerResponse.Nombre + " " + alquilerResponse.Placa + " " + alquilerResponse.TipoVehiculo + " " + alquilerResponse.Marca , true, true);
                doc.Replace("[PRECIO_DIA]", alquilerResponse.PrecioDia.ToString("N2", culture), true, true);
                doc.Replace("[FECHA_INI]", alquilerResponse.FechaInicio.ToString("dd/MM/yyyy"), true, true);
                doc.Replace("[EFCHA_FIN]", alquilerResponse.FechaFin.ToString("dd/MM/yyyy"), true, true);
                doc.Replace("[CANT_DIA]", alquilerResponse.CantidadDias.ToString(), true, true);
                doc.Replace("[TOTAL]", alquilerResponse.PrecioTotal.ToString("N2", culture), true, true);

                MemoryStream archivoMemoria = new MemoryStream();
                doc.SaveToStream(archivoMemoria, FileFormat.PDF);
                Byte[] ByteFile = archivoMemoria.ToArray();
                doc.Close();

                result.Success = true;
                result.ErrorMessage = Convert.ToBase64String(ByteFile);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = "No se tiene plantilla" + ex;
                return result;
            }

            return result;

        }



        public async Task<BaseResponseGeneric<AlquilerDtoResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<AlquilerDtoResponse>();
            try
            {
                var entity = await _alquilerRepository.FindByIdAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "Alquiler no encontrado";
                    return response;
                }

                response.Data = _mapper.Map<AlquilerDtoResponse>(entity);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al buscar el Alquiler por ID";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}
