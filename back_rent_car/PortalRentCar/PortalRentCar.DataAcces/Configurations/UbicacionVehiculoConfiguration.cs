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
    public class UbicacionVehiculoConfiguration : IEntityTypeConfiguration<UbicacionVehiculo>
    {
        public void Configure(EntityTypeBuilder<UbicacionVehiculo> builder)
        {
            builder.Property(p => p.FechaPosteo)
                .HasColumnType("DATE");
            builder.Property(p => p.Latitud)
                .HasColumnType("decimal(18,8)");
            builder.Property(p => p.Longitud)
                .HasColumnType("decimal(18,8)");
        }
    }
}
