using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalRentCar.Entities;
using PortalRentCar.Repositories.Inplementaciones;
using PortalRentCar.Repositories.Interfaces;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Implementaciones
{
    public class TipoVehiculoService : ITipoVehiculoService
    {
        private readonly ILogger<TipoVehiculoService> _logger;
        private readonly IMapper _mapper;
        private readonly ITipoVehiculoRepository _tipoVehiculoRepository;

        public TipoVehiculoService(ITipoVehiculoRepository tipoVehiculoRepository, ILogger<TipoVehiculoService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _tipoVehiculoRepository = tipoVehiculoRepository;
        }

        public async Task<BaseResponse> AddAsync(TipoVehiculoDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                await _tipoVehiculoRepository.AddAsync(_mapper.Map<TipoVehiculo>(request));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al registrar el tipo de Vehiculo ";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await _tipoVehiculoRepository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al eliminar  el tipo de Vehiculo";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<TipoVehiculoDtoRequest>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<TipoVehiculoDtoRequest>();
            try
            {
                var cargo = await _tipoVehiculoRepository.FindByIdAsync(id);
                if (cargo == null)
                {
                    response.ErrorMessage = "Tipo de Vehiculo no encontrado";
                    return response;
                }

                response.Data = _mapper.Map<TipoVehiculoDtoRequest>(cargo);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al buscar el tipo de Vehiculo por ID";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<TipoVehiculoDtoResponse>>> ListAsync()
        {
            var response = new BaseResponseGeneric<ICollection<TipoVehiculoDtoResponse>>();
            try
            {
                var collection = await _tipoVehiculoRepository.ListAsync();
                response.Data = _mapper.Map<ICollection<TipoVehiculoDtoResponse>>(collection);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar el tipo de vehiculo";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, TipoVehiculoDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var cargo = await _tipoVehiculoRepository.FindByIdAsync(id);
                if (cargo == null)
                {
                    response.ErrorMessage = "Tipo de Vehiculo no encontrado";
                    return response;
                }

                _mapper.Map(request, cargo);

                await _tipoVehiculoRepository.UpdateAsync();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al actualizar el Tipo de Vehiculo";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
    }
}
