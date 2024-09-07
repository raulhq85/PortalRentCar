using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);

        Task<BaseResponse> RegisterAsync(RegistrarUsuarioDto request);

        Task<BaseResponse> SendTokenToResetPasswordAsync(GenerateTokenToResetDtoRequest request);

        Task<BaseResponse> ResetPasswordAsync(ResetPasswordDtoRequest request);
    }
}
