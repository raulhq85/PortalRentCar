using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.DataAcces
{
    public class RentCarSecurityDbContext : IdentityDbContext<RentCarIdentityUser>
    {
        public RentCarSecurityDbContext(DbContextOptions<RentCarSecurityDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RentCarIdentityUser>(e =>
            {
                e.ToTable("Usuario");
                e.Property(p => p.LockoutEnd).HasColumnName("BloqueoHasta");
                e.Property(p => p.AccessFailedCount).HasColumnName("NroIntentos");
            });
            builder.Entity<IdentityRole>(e => e.ToTable("Perfil"));
            builder.Entity<IdentityUserRole<string>>(e => e.ToTable("UsuarioPerfil"));
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<string>()
                .HaveMaxLength(150);
            configurationBuilder.Conventions.Remove<CascadeDeleteConvention>();
        }
    }
}
