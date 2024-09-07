using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Interfaces
{
    public interface IClienteService
    {
        Task<PaginationResponse<ClienteDtoResponse>> ListAsync(ClienteSearchDtoRequest request);
        Task<BaseResponse> UpdateAsync(int id, ClienteDtoRequest request);
    }
}
