using PortalRentCar.DataAcces;
using PortalRentCar.Entities;
using PortalRentCar.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Repositories.Inplementaciones
{
    public class TipoVehiculoRepository(PortalRentCarDbContext context) : RepositoryBase<TipoVehiculo>(context),ITipoVehiculoRepository
    {
    }
}
