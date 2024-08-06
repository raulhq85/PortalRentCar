using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdUsuarioModifica { get; set; }

        protected EntityBase()
        {
            Estado = true;
            FechaCreacion = DateTime.UtcNow;
        }
    }
}
