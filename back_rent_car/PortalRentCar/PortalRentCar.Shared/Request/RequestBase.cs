using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Shared.Request
{
    public class RequestBase
    {
        private int _pagina = 1;
        private int _filas = 5;

        public int Pagina
        {
            get => _pagina;
            set => _pagina = value < 1 ? 1 : value;
        }

        public int Filas
        {
            get => _filas;
            set => _filas = value < 1 ? 5 : value;
        }
    }
}
