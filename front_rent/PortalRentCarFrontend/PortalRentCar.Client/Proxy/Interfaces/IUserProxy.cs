using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Client.Proxy.Interfaces
{
    public interface IUserProxy
    {
        Task<LoginDtoResponse> Login(LoginDtoRequest request);

        Task<HttpResponseMessage> Register(RegistrarUsuarioDto request);

        Task SendTokenToResetPassword(GenerateTokenToResetDtoRequest request);

        Task ResetPassword(ResetPasswordDtoRequest request);
    }
}
