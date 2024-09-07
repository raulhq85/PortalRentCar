using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Shared.Request
{
    public class VehiculoDtoRequest
    {
        public int? Id { get; set; }
        public int TipoVehiculoId { get; set; }
        public int MarcaId { get; set; }
        public string Nombre { get; set; } = default!;
        public string Color { get; set; } = default!;
        public int Anio { get; set; }
        public string Placa { get; set; } = default!;
        public int Kilometraje { get; set; }
        public decimal Precio { get; set; }
        public int SituacionVehiculo { get; set; }
        public string? Base64Imagen { get; set; }
        public string? ImagenUrL { get; set; }
        public string? ArchivoImagen { get; set; }
    }
}
