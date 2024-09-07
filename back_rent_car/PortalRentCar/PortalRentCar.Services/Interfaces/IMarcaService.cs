using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Interfaces
{
    public interface IMarcaService
    {
        Task<BaseResponseGeneric<ICollection<MarcaDtoResponse>>> ListAsync();

        Task<BaseResponseGeneric<MarcaDtoRequest>> FindByIdAsync(int id);

        Task<BaseResponse> AddAsync(MarcaDtoRequest request);

        Task<BaseResponse> UpdateAsync(int id, MarcaDtoRequest request);

        Task<BaseResponse> DeleteAsync(int id);

    }
}
