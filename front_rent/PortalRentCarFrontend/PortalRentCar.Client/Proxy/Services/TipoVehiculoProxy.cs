using PortalRentCar.Client.Proxy.Interfaces;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Client.Proxy.Services
{
    public class TipoVehiculoProxy : CrudRestHelperBase<TipoVehiculoDtoRequest, TipoVehiculoDtoResponse>, ITipoVehiculoProxy
    {
        public TipoVehiculoProxy(HttpClient httpClient)
            : base("api/TipoVehiculo", httpClient)
        {
        }
    }
}
