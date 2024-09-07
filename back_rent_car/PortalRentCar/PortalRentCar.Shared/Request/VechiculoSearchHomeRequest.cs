﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Shared.Request
{
    public class VehiculoSearchHomeRequest : RequestBase
    {
        public int? TipoVehiculoId { get; set; }
        public int? MarcaId { get; set; }
        public string? Vehiculo { get; set; }
        public string? Color { get; set; }
        public int? Anio { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }
    }
}
