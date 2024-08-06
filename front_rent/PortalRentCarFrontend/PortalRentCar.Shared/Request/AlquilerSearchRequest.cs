using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Shared.Request
{
    public class AlquilerSearchRequest : RequestBase
    {
        public int? ClienteId { get; set; }
        public string? Placa { get; set; }
        public int? TipoVehiculoId { get; set; }
        public int? MarcaId { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }
    }
}
