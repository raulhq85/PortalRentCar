using PortalRentCar.Entities;
using PortalRentCar.Entities.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Repositories.Interfaces
{
    public interface IUbicacionVehiculoRepository : IRepositoryBase<UbicacionVehiculo>
    {
        Task<ICollection<UbicacionVehiculoInfo>> ListAsyncUbicacionVehiculo();

        Task<UbicacionVehiculoInfo> GetAsyncUbicacionByIdVehicleAsync(int id);
    }
}
