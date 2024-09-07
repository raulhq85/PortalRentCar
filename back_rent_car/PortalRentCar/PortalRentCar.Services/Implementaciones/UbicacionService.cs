using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalRentCar.Repositories.Inplementaciones;
using PortalRentCar.Repositories.Interfaces;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Implementaciones
{
    public class UbicacionService : IUbicacionService
    {
        private readonly ILogger<UbicacionService> _logger;
        private readonly IMapper _mapper;
        private readonly IUbicacionVehiculoRepository _iUbicacionVehiculoRepository;

        public UbicacionService(IUbicacionVehiculoRepository iUbicacionVehiculoRepository, ILogger<UbicacionService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _iUbicacionVehiculoRepository = iUbicacionVehiculoRepository;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await _iUbicacionVehiculoRepository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al eliminar  la ubicacion";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<UbicacionVehiculoDtoResponse>>> GetListUbicacionVehiculo()
        {

            var response = new BaseResponseGeneric<ICollection<UbicacionVehiculoDtoResponse>>();
            try
            {
                var collection = await _iUbicacionVehiculoRepository.ListAsyncUbicacionVehiculo();
                response.Data = _mapper.Map<ICollection<UbicacionVehiculoDtoResponse>>(collection);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar el tipo de vehiculo";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;

        }
    }
}
