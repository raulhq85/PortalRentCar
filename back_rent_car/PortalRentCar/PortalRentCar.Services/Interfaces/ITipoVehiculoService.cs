using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Interfaces
{
    public interface ITipoVehiculoService
    {
        Task<BaseResponseGeneric<ICollection<TipoVehiculoDtoResponse>>> ListAsync();

        Task<BaseResponseGeneric<TipoVehiculoDtoRequest>> FindByIdAsync(int id);

        Task<BaseResponse> AddAsync(TipoVehiculoDtoRequest request);

        Task<BaseResponse> UpdateAsync(int id, TipoVehiculoDtoRequest request);

        Task<BaseResponse> DeleteAsync(int id);

        //Task<BaseResponse> AddAsync2(TipoVehiculoDtoRequest request);

    }
}
