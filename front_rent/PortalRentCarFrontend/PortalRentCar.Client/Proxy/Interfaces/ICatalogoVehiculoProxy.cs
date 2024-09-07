using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Client.Proxy.Interfaces;

public interface ICatalogoVehiculoProxy
{
    Task<PaginationResponse<VehiculoHomeDtoResponse>> ListarVehiculosHomeAsync(VehiculoSearchHomeRequest request);

    Task<BaseResponseGeneric<VehiculoHomeDtoResponse>> GetVehiculoHomeByIdAsync(int id);
}