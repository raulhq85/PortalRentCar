using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Client.Proxy.Interfaces
{
    public interface IMarcaProxy : ICrudRestHelper<MarcaDtoRequest, MarcaDtoResponse>
    {
    }
}
