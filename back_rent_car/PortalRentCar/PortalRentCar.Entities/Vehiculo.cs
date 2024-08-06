using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Entities
{
    public class Vehiculo : EntityBase
    {
        public TipoVehiculo TipoVehiculo { get; set; } = default!;
        public int TipoVehiculoId { get; set; }
        public Marca Marca { get; set; } = default!;
        public int MarcaId { get; set; }
        public string Nombre { get; set; } = default!;
        public string Color { get; set; } = default!;
        public int Anio { get; set; }
        public string Placa { get; set; } = default!;
        public int Kilometraje { get; set; }
        public decimal Precio { get; set; }
        public string? ImagenUrL {  get; set; }
        public SituacionVehiculo SituacionVehiculo { get; set; }
        public ICollection<Alquiler> Alquileres { get; set; } = new List<Alquiler>();
    }
}
