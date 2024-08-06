using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Entities.Infos
{
    public class ClienteInfo
    {
        public int Id { get; set; }
        public string NroDocumento { get; set; } = default!;
        public string RazonSocial { get; set; } = default!;
        public string Correo { get; set; } = default!;
        public string Telefono { get; set; } = default!;
        //public string Departamento { get; set; } = default!;
        //public string Provincia { get; set; } = default!;
        //public string Distrito { get; set; } = default!;
    }
}
