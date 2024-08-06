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
    public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
    {
        public void Configure(EntityTypeBuilder<Vehiculo> builder)
        {
            builder.HasQueryFilter(p => p.Estado);
            builder.Property(p => p.FechaCreacion)
                .HasColumnType("DATE");
            builder.Property(p => p.Precio)
                .HasColumnType("decimal(7,2)");
            builder.Property(p => p.ImagenUrL)
                .IsUnicode(false);
        }
    }
}
