using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Client.Proxy.Interfaces
{
    public interface IVehiculoProxy : ICrudRestHelper<VehiculoDtoRequest, VehiculoDtoResponse>
    {
        Task<PaginationResponse<VehiculoDtoResponse>> ListAsync(VehiculoSearchRequest request);
        Task<ICollection<UbicacionVehiculoDtoResponse>> GetListVehiculoUbicacion();
    }
}
