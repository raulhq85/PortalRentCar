using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Shared.Response
{
    public class UbicacionVehiculoDtoResponse
    {
        public string nombre { get; set; } = default!;
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public DateTime FechaPosteo { get; set; }
        public string TipoVehiculo { get; set; } = default!;
        public string Marca { get; set; } = default!;
        public string Color { get; set; } = default!;
        public int Anio { get; set; }
        public string Placa { get; set; } = default!;
        public int Kilometraje { get; set; }
        public decimal Precio { get; set; }
        public string? ImagenUrL { get; set; }
        public string Cliente { get; set; } = default!;
        public string NroAlquiler { get; set; } = default!;
    }
}
