using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Interfaces
{
    public interface IAlquilerService
    {
        Task<PaginationResponse<AlquilerDtoResponse>> ListAsync(AlquilerSearchRequest request, string cliente);
        Task<BaseResponse> AddAsync(string usuarioMail, AlquilerDtoRequest request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponse> GetDocumentAlquilerByIdAsync(int id);
        Task<BaseResponseGeneric<AlquilerDtoResponse>> FindByIdAsync(int id);
    }
}
