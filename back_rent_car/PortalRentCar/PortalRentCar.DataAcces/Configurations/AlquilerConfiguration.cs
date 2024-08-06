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
    public class AlquilerConfiguration : IEntityTypeConfiguration<Alquiler>
    {
        public void Configure(EntityTypeBuilder<Alquiler> builder)
        {
            builder.Property(p => p.FechaCreacion)
                .HasColumnType("DATE");
            builder.HasQueryFilter(p => p.Estado);
            builder.Property(p => p.NroAlquiler)
                .HasMaxLength(20);
            builder.HasIndex(p => p.NroAlquiler);
            builder.Property(p => p.FechaInicio)
               .HasColumnType("DATE");
            builder.Property(p => p.FechaFin)
               .HasColumnType("DATE");
            builder.Property(p => p.PrecioDia)
                .HasColumnType("decimal(7,2)");
            builder.Property(p => p.PrecioTotal)
                .HasColumnType("decimal(7,2)");

        }
    }
}
