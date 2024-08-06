using AutoMapper;
using Microsoft.Extensions.Logging;
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
    public class ClienteService : IClienteService
    {

        private readonly ILogger<ClienteService> _logger;
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository, ILogger<ClienteService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        public async Task<PaginationResponse<ClienteDtoResponse>> ListAsync(ClienteSearchDtoRequest request)
        {
            var response = new PaginationResponse<ClienteDtoResponse>();
            try
            {
                var tupla = await _clienteRepository.ListClienteByParametersAsync(request.Cliente, request.ClienteId, request.NroDocumento, request.Pagina, request.Filas);

                response.Data = _mapper.Map<ICollection<ClienteDtoResponse>>(tupla.Collection);
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

        public async Task<BaseResponse> UpdateAsync(int id, ClienteDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var entity = await _clienteRepository.FindByIdAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "No se encontró al cliente";
                    return response;
                }

                _mapper.Map(request, entity);

                await _clienteRepository.UpdateAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al actualizar al cliente";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}
