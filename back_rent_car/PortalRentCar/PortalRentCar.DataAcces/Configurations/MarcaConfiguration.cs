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
    public class MarcaConfiguration : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.HasQueryFilter( p => p.Estado);
            builder.Property(p => p.FechaCreacion)
                .HasColumnType("DATE");

            var fecha = DateTime.Parse("2024-07-28");

            builder.HasData(new List<Marca>
            {
                new() { Id = 1, Nombre = "Toyota", FechaCreacion = fecha },
                new() { Id = 2, Nombre = "Honda", FechaCreacion = fecha },
                new() { Id = 3, Nombre = "Ford", FechaCreacion = fecha },
                new() { Id = 4, Nombre = "Chevrolet", FechaCreacion = fecha }
            });
        }
    }
}
