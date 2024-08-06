using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Shared.Request
{
    public class ClienteSearchDtoRequest : RequestBase
    {
        public string? Cliente { get; set; }
        public int? ClienteId { get; set; }
        public string? NroDocumento { get; set; }
    }
}
