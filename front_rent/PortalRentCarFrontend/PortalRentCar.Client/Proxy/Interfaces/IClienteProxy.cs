

using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Client.Proxy.Interfaces
{
    public interface IClienteProxy : ICrudRestHelper<ClienteDtoRequest, ClienteDtoResponse>
    {
        Task<PaginationResponse<ClienteDtoResponse>> ListAsync(ClienteSearchDtoRequest request);
    }
}
