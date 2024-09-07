using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Interfaces
{
    public interface IUbicacionService
    {
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponseGeneric<ICollection<UbicacionVehiculoDtoResponse>>> GetListUbicacionVehiculo();
    }
}
