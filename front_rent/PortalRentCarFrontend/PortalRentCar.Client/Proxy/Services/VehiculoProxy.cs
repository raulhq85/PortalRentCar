using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using PortalRentCar.Client.Proxy.Interfaces;
using System.Net.Http.Json;

namespace PortalRentCar.Client.Proxy.Services
{
    public class VehiculoProxy : CrudRestHelperBase<VehiculoDtoRequest, VehiculoDtoResponse>, IVehiculoProxy
    {
        public VehiculoProxy(HttpClient httpClient)
            : base("api/Vehiculo", httpClient)
        {
        }

        public async Task<ICollection<UbicacionVehiculoDtoResponse>> GetListVehiculoUbicacion()
        {

            var response = await HttpClient.GetFromJsonAsync<PaginationResponse<UbicacionVehiculoDtoResponse>>("api/Vehiculo/GetListVehiculoUbicacion");

            if (response != null && response.Success)
            {
                return response.Data!;
            }

            throw new InvalidOperationException(response?.ErrorMessage ?? "Error al obtener ubicaciones de vehículos.");




            //// Hacer una solicitud GET al endpoint que devuelve una lista de ubicaciones de vehículos
            //var ubicaciones = await HttpClient.GetFromJsonAsync<ICollection<UbicacionVehiculoDtoResponse>>("api/Vehiculo/GetListVehiculoUbicacion");

            //// Verificar si la respuesta es null
            //if (ubicaciones != null)
            //{
            //    return ubicaciones;
            //}

            //// Si la respuesta es null, lanzar una excepción o manejarlo de alguna otra manera
            //throw new InvalidOperationException("Error al obtener ubicaciones de vehículos.");

        }

        public async Task<PaginationResponse<VehiculoDtoResponse>> ListAsync(VehiculoSearchRequest request)
        {
            var response =
                await ListAsync(
                    $"?TipoVehiculoId={request.TipoVehiculoId}&MarcaId={request.MarcaId}&Nombre={request.Nombre}&Color={request.Color}&Anio={request.Anio}&PrecioMinimo={request.PrecioMinimo}&PrecioMaximo={request.PrecioMinimo}&SituacionVehiculo={request.SituacionVehiculo}&pagina={request.Pagina}&filas={request.Filas}");

            if (response is { Success: true })
            {
                Console.WriteLine(response);

                return response;
            }

            return await Task.FromResult(new PaginationResponse<VehiculoDtoResponse>());
        }
    }
}
