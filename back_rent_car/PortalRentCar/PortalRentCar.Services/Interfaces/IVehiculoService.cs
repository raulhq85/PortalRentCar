using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Interfaces
{
    public interface IVehiculoService
    {
        Task<PaginationResponse<VehiculoDtoResponse>> ListAsync(VehiculoSearchRequest request);
        Task<BaseResponse> AddAsync(VehiculoDtoRequest request);
        Task<BaseResponse> UpdateAsync(int id, VehiculoDtoRequest request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponseGeneric<VehiculoDtoRequest>> FindByIdAsync(int id);
        Task<PaginationResponse<VehiculoHomeDtoResponse>> ListarVehiculosHomeAsync(VehiculoSearchHomeRequest request);
        Task<BaseResponseGeneric<VehiculoHomeDtoResponse>> GetVehiculoHomeAsyncById(int id);
        //Task<BaseResponseGeneric<ICollection<UbicacionVehiculoDtoResponse>>> GetListUbicacionVehiculo();
    }
}
