using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalRentCar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.DataAcces.Configurations
{
    public class TipoVehiculoConfiguration : IEntityTypeConfiguration<TipoVehiculo>
    {
        public void Configure(EntityTypeBuilder<TipoVehiculo> builder)
        {
            var fecha = DateTime.Parse("2024-07-28");

            builder.HasData(new List<TipoVehiculo>
            {
                new() { Id = 1, Nombre = "Sedan", FechaCreacion = fecha},
                new() { Id = 2, Nombre = "HashBack", FechaCreacion = fecha },
                new() { Id = 3, Nombre = "MiniVan", FechaCreacion = fecha }
            });
            builder.HasQueryFilter(p => p.Estado);
        }

    }
}
