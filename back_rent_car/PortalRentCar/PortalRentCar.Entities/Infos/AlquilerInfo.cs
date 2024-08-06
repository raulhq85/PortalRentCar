using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Entities.Infos
{
    public class AlquilerInfo
    {
        public int Id { get; set; }
        public string NroAlquiler { get; set; } = default!;
        public DateTime Fecha {  get; set; }
        public string Cliente { get; set; } = default!;
        public string TipoVehiculo { get; set; } = default!;
        public string Placa { get; set; } = default!;
        public int CantidadDias { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin {  get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
