using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Entities.Infos
{
    public class UbicacionInfo
    {
        public int Id { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public DateTime FechaPosteo { get; set; }
        public string TipoVehiculo { get; set; } = default!;
        public string Marca { get; set; } = default!;
        public string Color { get; set; } = default!;
        public int Anio { get; set; }
        public string Placa { get; set; } = default!;
        public int Kilometraje { get; set; }
        public decimal Precio { get; set; }
        public string? ImagenUrL { get; set; }
        public string SituacionVehiculo { get; set; } = default!;
    }
}
