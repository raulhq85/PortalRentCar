using PortalRentCar.Client.Proxy.Interfaces;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System.Net.Http.Json;

namespace PortalRentCar.Client.Proxy.Services
{
    public class AlquilerProxy : CrudRestHelperBase<AlquilerDtoRequest, AlquilerDtoResponse>, IAlquilerProxy
    {
        public AlquilerProxy(HttpClient httpClient)
        : base("api/ALquiler", httpClient)
        {
        }

        public async Task<string> GenerateAlquilerAsync(AlquilerDtoRequest request)
        {
            var response = await HttpClient.PostAsJsonAsync($"{BaseUrl}/GenerateAlquilerAsync", request);
            var result = await response.Content.ReadFromJsonAsync<BaseResponse>();
            if (result is { Success: false })
            {
                throw new InvalidOperationException(result.ErrorMessage);
            }
            return result?.ErrorMessage ?? throw new InvalidOperationException("Número de venta no disponible");
        }

        public async Task<BaseResponse> GetDocumentAlquilerByIdAsync(int id)
        {
            var response = await HttpClient.GetAsync($"{BaseUrl}/GetDocumentAlquilerByIdAsync?id={id}");
            var result = await response.Content.ReadFromJsonAsync<BaseResponse>();
            return result;
            //try
            //{
            //    var response = await HttpClient.GetAsync($"{BaseUrl}/GetDocumentAlquilerByIdAsync?id={id}");

            //    // Verificar si la respuesta es exitosa
            //    if (response.IsSuccessStatusCode)
            //    {
            //        // Verificar si la respuesta tiene contenido
            //        if (response.Content != null)
            //        {
            //            var result = await response.Content.ReadFromJsonAsync<BaseResponse>();
            //            return result;
            //        }
            //        else
            //        {
            //            throw new InvalidOperationException("La respuesta de la API está vacía.");
            //        }
            //    }
            //    else
            //    {
            //        throw new InvalidOperationException($"Error en la respuesta de la API: {response.StatusCode}");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Manejar errores de solicitud y deserialización
            //    throw new InvalidOperationException($"Error al consultar la API: {ex.Message}");
            //}
        }

        public async  Task<PaginationResponse<AlquilerDtoResponse>> ListAsync(AlquilerSearchRequest request)
        {
            var response =
                await ListAsync(
                    $"?Vehiculo={request.Vehiculo}&Cliente={request.Cliente}&NroAlquiler={request.NroAlquiler}&Placa={request.Placa}&TipoVehiculoId={request.TipoVehiculoId}&MarcaId={request.MarcaId}&PrecioMinimo={request.PrecioMinimo}&PrecioMaximo={request.PrecioMaximo}&pagina={request.Pagina}&filas={request.Filas}");

            if (response is { Success: true })
            {
                return response;
            }

            return await Task.FromResult(new PaginationResponse<AlquilerDtoResponse>());
        }

    }
}
