using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Client.Proxy.Interfaces
{
    public interface IAlquilerProxy : ICrudRestHelper<AlquilerDtoRequest,AlquilerDtoResponse>
    {
        Task<PaginationResponse<AlquilerDtoResponse>> ListAsync(AlquilerSearchRequest request);

        Task<string> GenerateAlquilerAsync(AlquilerDtoRequest request);

        Task<BaseResponse> GetDocumentAlquilerByIdAsync(int id);
    }
}
