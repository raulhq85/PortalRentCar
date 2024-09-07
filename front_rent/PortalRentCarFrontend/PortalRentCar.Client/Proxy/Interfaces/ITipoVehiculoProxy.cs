using PortalRentCar.Client.Proxy.Interfaces;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Client.Proxy.Interfaces
{
    public interface ITipoVehiculoProxy : ICrudRestHelper<TipoVehiculoDtoRequest, TipoVehiculoDtoResponse>
    {
    }
}
