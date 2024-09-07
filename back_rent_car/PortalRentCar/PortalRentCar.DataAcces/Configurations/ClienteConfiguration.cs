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
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.Property(p => p.FechaCreacion)
                .HasColumnType("DATE");
            builder.Property(p => p.NroDocumento)
                .HasMaxLength(12);
            builder.HasIndex(p => p.NroDocumento);
            builder.HasQueryFilter(P => P.Estado);
        }
    }
}
