using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Entities.Infos
{
    public class VehiculoHomeInfo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string TipoVehiculo { get; set; } = default!;
        public string Marca { get; set; } = default!;
        public string Color { get; set; } = default!;
        public int Anio { get; set; }
        public string Placa { get; set; } = default!;
        public int Kilometraje { get; set; }
        public decimal Precio { get; set; }
        public string? ImagenUrL { get; set; }
    }
}
