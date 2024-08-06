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
    public class MarcaService : IMarcaService
    {
        private readonly ILogger<MarcaService> _logger;
        private readonly IMapper _mapper;
        private readonly IMarcaRepository _marcaRepository;

        public MarcaService(IMarcaRepository marcaRepository, ILogger<MarcaService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _marcaRepository = marcaRepository;
        }


        public async Task<BaseResponse> AddAsync(MarcaDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                await _marcaRepository.AddAsync(_mapper.Map<Marca>(request));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al registrar el tipo de marca ";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await _marcaRepository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al eliminar  el tipo de Marca";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<MarcaDtoRequest>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<MarcaDtoRequest>();
            try
            {
                var cargo = await _marcaRepository.FindByIdAsync(id);
                if (cargo == null)
                {
                    response.ErrorMessage = "Tipo de Marca no encontrado";
                    return response;
                }

                response.Data = _mapper.Map<MarcaDtoRequest>(cargo);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al buscar el tipo de Marca por ID";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<MarcaDtoResponse>>> ListAsync()
        {
            var response = new BaseResponseGeneric<ICollection<MarcaDtoResponse>>();
            try
            {
                var collection = await _marcaRepository.ListAsync();
                response.Data = _mapper.Map<ICollection<MarcaDtoResponse>>(collection);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar el tipo de Marca";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, MarcaDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var cargo = await _marcaRepository.FindByIdAsync(id);
                if (cargo == null)
                {
                    response.ErrorMessage = "Tipo de Marca no encontrado";
                    return response;
                }

                _mapper.Map(request, cargo);

                await _marcaRepository.UpdateAsync();

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
