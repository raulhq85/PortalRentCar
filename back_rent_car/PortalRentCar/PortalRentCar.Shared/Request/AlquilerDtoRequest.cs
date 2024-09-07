using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Shared.Request
{
    public class AlquilerDtoRequest
    {
        public int VehiculoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal PrecioDia { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
