using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Entities
{
    public class Alquiler : EntityBase
    {
        public string NroAlquiler { get; set; } = default!;
        public Cliente Cliente { get; set; } = default!;
        public int ClienteId { get; set; }
        public Vehiculo Vehiculo { get; set; } = default!;
        public int VehiculoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin {  get; set; }
        public decimal PrecioDia { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
