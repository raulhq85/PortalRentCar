using PortalRentCar.Client.Proxy.Interfaces;
using System.Net.Http.Json;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Client.Proxy.Services
{
    public class UserProxy : IUserProxy
    {
        private readonly HttpClient _httpClient;

        public UserProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginDtoResponse> Login(LoginDtoRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Usuario/login", request);
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginDtoResponse>();

            return loginResponse!;
        }

        public async Task<HttpResponseMessage> Register(RegistrarUsuarioDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Usuario/Register", request);

            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadFromJsonAsync<BaseResponse>();
                if (resultado != null && resultado.Success == false)
                    throw new InvalidOperationException(resultado.ErrorMessage);
            }
            else
            {
                throw new InvalidOperationException(response.ReasonPhrase);
            }

            return response;
        }

        public async Task SendTokenToResetPassword(GenerateTokenToResetDtoRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Usuario/SendTokenToResetPassword", request);
            if (!response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadFromJsonAsync<BaseResponse>();
                throw new InvalidOperationException(jsonResponse!.ErrorMessage);
            }
        }

        public async Task ResetPassword(ResetPasswordDtoRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Usuario/ResetPassword", request);
            if (!response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadFromJsonAsync<BaseResponse>();
                throw new InvalidOperationException(jsonResponse!.ErrorMessage);
            }
        }
    }
}
