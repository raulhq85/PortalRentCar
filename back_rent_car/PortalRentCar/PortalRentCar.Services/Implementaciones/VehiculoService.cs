using AutoMapper;
using Microsoft.Extensions.Logging;
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
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Implementaciones
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly ILogger<VehiculoService> _logger;
        private readonly IMapper _mapper;
        private readonly IFileUploader _fileUploader;

        public VehiculoService(IVehiculoRepository vehiculoRepository, ILogger<VehiculoService> logger, IMapper mapper, IFileUploader fileUploader)
        {
            _vehiculoRepository = vehiculoRepository;
            _logger = logger;
            _mapper = mapper;
            _fileUploader = fileUploader;
        }

        public async Task<BaseResponse> AddAsync(VehiculoDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var entity = _mapper.Map<Vehiculo>(request);

                entity.ImagenUrL = await _fileUploader.UploadFileAsync(request.Base64Imagen, request.ArchivoImagen);
               // entity.TemarioUrl = await _fileUploader.UploadFileAsync(request.Base64Temario, request.ArchivoTemario);

                await _vehiculoRepository.AddAsync(entity);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al agregar el Vehiculo";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var entity = await _vehiculoRepository.FindByIdAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "No se encontró el Vehiculo";
                    return response;
                }

                await _vehiculoRepository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al eliminar el Vehiculo";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<VehiculoDtoRequest>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<VehiculoDtoRequest>();
            try
            {
                var entity = await _vehiculoRepository.FindByIdAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "Vehiculo no encontrado";
                    return response;
                }

                response.Data = _mapper.Map<VehiculoDtoRequest>(entity);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al buscar el Vehiculo por ID";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        //public async Task<BaseResponseGeneric<ICollection<UbicacionVehiculoDtoResponse>>> GetListUbicacionVehiculo()
        //{
        //    var response = new BaseResponseGeneric<ICollection<UbicacionVehiculoDtoResponse>>();
        //    try
        //    {
        //        var collection = await _vehiculoRepository.ListAsyncUbicacionVehiculo();
        //        response.Data = _mapper.Map<ICollection<UbicacionVehiculoDtoResponse>>(collection);
        //        response.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorMessage = "Error al listar el tipo de vehiculo";
        //        _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        //    }
        //    return response;
        //}

        public async Task<BaseResponseGeneric<VehiculoHomeDtoResponse>> GetVehiculoHomeAsyncById(int id)
        {
            var response = new BaseResponseGeneric<VehiculoHomeDtoResponse>();
            try
            {
                var entity = await _vehiculoRepository.GetVehiculoHomeByIdAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "No se encontró el Vehiculo";
                    return response;
                }

                response.Data = _mapper.Map<VehiculoHomeDtoResponse>(entity);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al buscar el Vehiculo";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<PaginationResponse<VehiculoHomeDtoResponse>> ListarVehiculosHomeAsync(VehiculoSearchHomeRequest request)
        {
            var response = new PaginationResponse<VehiculoHomeDtoResponse>();

            try
            {
                var tupla = await _vehiculoRepository.ListarVehiculoHomeAsync(request.Vehiculo, request.TipoVehiculoId, request.MarcaId, request.Anio, request.PrecioMinimo, request.PrecioMaximo,  request.Pagina, request.Filas);

                response.Data = _mapper.Map<ICollection<VehiculoHomeDtoResponse>>(tupla.Collection);
                response.TotalPages = Helper.GetTotalPages(tupla.Total, request.Filas);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los Vehiculos";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<PaginationResponse<VehiculoDtoResponse>> ListAsync(VehiculoSearchRequest request)
        {
            var response = new PaginationResponse<VehiculoDtoResponse>();
            try
            {
                var tupla = await _vehiculoRepository.ListarVehiculoByParametersAsync(request.Nombre, request.TipoVehiculoId, request.MarcaId, request.Anio, request.PrecioMinimo, request.PrecioMaximo, request.SituacionVehiculo, request.Pagina, request.Filas);

                response.Data = _mapper.Map<ICollection<VehiculoDtoResponse>>(tupla.Collection);
                response.TotalPages = Helper.GetTotalPages(tupla.Total, request.Filas);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los Vehiculos";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, VehiculoDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var entity = await _vehiculoRepository.FindByIdAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "No se encontró el Vehiculo";
                    return response;
                }

                _mapper.Map(request, entity);

                if (request.Base64Imagen != null)
                {
                    entity.ImagenUrL = await _fileUploader.UploadFileAsync(request.Base64Imagen, request.ArchivoImagen);
                }

                await _vehiculoRepository.UpdateAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al actualizar el Vehiculo";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

       


    }
}
