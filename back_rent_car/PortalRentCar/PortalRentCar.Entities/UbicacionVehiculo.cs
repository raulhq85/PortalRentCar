using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Entities
{
    public class UbicacionVehiculo : EntityBase
    {
        public Vehiculo Vehiculo { get; set; } = default!;
        public int VehiculoId { get; set; }
        public double Latitud {  get; set; }
        public double Longitud { get; set; }
        public DateTime FechaPosteo { get; set; }
    }
}
