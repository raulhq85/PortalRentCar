using PortalRentCar.Client.Proxy.Interfaces;
using System.Net.Http.Json;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Client.Proxy.Services;

public class CatalogoVehiculoProxy : RestBase, ICatalogoVehiculoProxy
{
    public CatalogoVehiculoProxy(HttpClient httpClient)
        : base("api/Catalogo", httpClient)
    {
    }

    public async Task<BaseResponseGeneric<VehiculoHomeDtoResponse>> GetVehiculoHomeByIdAsync(int id)
    {
        var response = await HttpClient.GetFromJsonAsync<BaseResponseGeneric<VehiculoHomeDtoResponse>>($"{BaseUrl}/{id}");

        if (response is { Success: false })
            throw new ApplicationException(response.ErrorMessage);

        return response!;
    }

    public async Task<PaginationResponse<VehiculoHomeDtoResponse>> ListarVehiculosHomeAsync(VehiculoSearchHomeRequest request)
    {
        var response = await HttpClient.GetFromJsonAsync<PaginationResponse<VehiculoHomeDtoResponse>>($"{BaseUrl}?TipoVehiculoId={request.TipoVehiculoId}&MarcaId={request.MarcaId}&Vehiculo={request.Vehiculo}&Color={request.Color}&Anio={request.Anio}&PrecioMinimo={request.PrecioMinimo}&PrecioMaximo={request.PrecioMaximo}&Pagina={request.Pagina}&Filas={request.Filas}");

        if (response is { Success: false })
            throw new ApplicationException(response.ErrorMessage);

        return response!;
    }
}