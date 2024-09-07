using PortalRentCar.Client.Proxy.Interfaces;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Client.Proxy.Services
{
    public class MarcaProxy : CrudRestHelperBase<MarcaDtoRequest, MarcaDtoResponse>, IMarcaProxy
    {
        public MarcaProxy(HttpClient httpClient)
            : base("api/Marca", httpClient)
        {
        }
    }
}
