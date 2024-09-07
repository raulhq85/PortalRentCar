﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortalRentCar.DataAcces;

#nullable disable

namespace PortalRentCar.DataAcces.Migrations.PortalRentCarDb
{
    [DbContext(typeof(PortalRentCarDbContext))]
    [Migration("20240813060741_SP_PDF_ALQUILER")]
    partial class SP_PDF_ALQUILER
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PortalRentCar.Entities.Alquiler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("DATE");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("DATE");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("DATE");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuarioModifica")
                        .HasColumnType("int");

                    b.Property<string>("NroAlquiler")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("PrecioDia")
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal>("PrecioTotal")
                        .HasColumnType("decimal(7,2)");

                    b.Property<int>("VehiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("NroAlquiler");

                    b.HasIndex("VehiculoId");

                    b.ToTable("Alquiler");
                });

            modelBuilder.Entity("PortalRentCar.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("DATE");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuarioModifica")
                        .HasColumnType("int");

                    b.Property<string>("NroDocumento")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("NroDocumento");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("PortalRentCar.Entities.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("DATE");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuarioModifica")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Marca");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Toyota"
                        },
                        new
                        {
                            Id = 2,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Honda"
                        },
                        new
                        {
                            Id = 3,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Ford"
                        },
                        new
                        {
                            Id = 4,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Chevrolet"
                        },
                        new
                        {
                            Id = 5,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "BMW"
                        },
                        new
                        {
                            Id = 6,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Mercedes-Benz"
                        },
                        new
                        {
                            Id = 7,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Audi"
                        },
                        new
                        {
                            Id = 8,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Volkswagen"
                        },
                        new
                        {
                            Id = 9,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Nissan"
                        },
                        new
                        {
                            Id = 10,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Hyundai"
                        },
                        new
                        {
                            Id = 11,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Kia"
                        },
                        new
                        {
                            Id = 12,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Subaru"
                        },
                        new
                        {
                            Id = 13,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Tesla"
                        },
                        new
                        {
                            Id = 14,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Mazda"
                        });
                });

            modelBuilder.Entity("PortalRentCar.Entities.TipoVehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuarioModifica")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TipoVehiculo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Sedan"
                        },
                        new
                        {
                            Id = 2,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Hatchback"
                        },
                        new
                        {
                            Id = 3,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Minivan"
                        },
                        new
                        {
                            Id = 4,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "SUV"
                        },
                        new
                        {
                            Id = 5,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Coupe"
                        },
                        new
                        {
                            Id = 6,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Convertible"
                        },
                        new
                        {
                            Id = 7,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Pickup"
                        },
                        new
                        {
                            Id = 8,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Crossover"
                        },
                        new
                        {
                            Id = 9,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Station Wagon"
                        },
                        new
                        {
                            Id = 10,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Roadster"
                        },
                        new
                        {
                            Id = 11,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Van"
                        },
                        new
                        {
                            Id = 12,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Sport"
                        },
                        new
                        {
                            Id = 13,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Luxury"
                        },
                        new
                        {
                            Id = 14,
                            Estado = true,
                            FechaCreacion = new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FechaModificacion = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdUsuarioModifica = 0,
                            Nombre = "Electric"
                        });
                });

            modelBuilder.Entity("PortalRentCar.Entities.UbicacionVehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaPosteo")
                        .HasColumnType("DATE");

                    b.Property<int>("IdUsuarioModifica")
                        .HasColumnType("int");

                    b.Property<decimal>("Latitud")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("Longitud")
                        .HasColumnType("decimal(18,8)");

                    b.Property<int>("VehiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehiculoId");

                    b.ToTable("UbicacionVehiculo");
                });

            modelBuilder.Entity("PortalRentCar.Entities.Vehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Anio")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("DATE");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuarioModifica")
                        .HasColumnType("int");

                    b.Property<string>("ImagenUrL")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Kilometraje")
                        .HasColumnType("int");

                    b.Property<int>("MarcaId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(7,2)");

                    b.Property<int>("SituacionVehiculo")
                        .HasColumnType("int");

                    b.Property<int>("TipoVehiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MarcaId");

                    b.HasIndex("TipoVehiculoId");

                    b.ToTable("Vehiculo");
                });

            modelBuilder.Entity("PortalRentCar.Entities.Alquiler", b =>
                {
                    b.HasOne("PortalRentCar.Entities.Cliente", "Cliente")
                        .WithMany("Alquileres")
                        .HasForeignKey("ClienteId")
                        .IsRequired();

                    b.HasOne("PortalRentCar.Entities.Vehiculo", "Vehiculo")
                        .WithMany("Alquileres")
                        .HasForeignKey("VehiculoId")
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("PortalRentCar.Entities.UbicacionVehiculo", b =>
                {
                    b.HasOne("PortalRentCar.Entities.Vehiculo", "Vehiculo")
                        .WithMany()
                        .HasForeignKey("VehiculoId")
                        .IsRequired();

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("PortalRentCar.Entities.Vehiculo", b =>
                {
                    b.HasOne("PortalRentCar.Entities.Marca", "Marca")
                        .WithMany("Vehiculos")
                        .HasForeignKey("MarcaId")
                        .IsRequired();

                    b.HasOne("PortalRentCar.Entities.TipoVehiculo", "TipoVehiculo")
                        .WithMany("Vehiculos")
                        .HasForeignKey("TipoVehiculoId")
                        .IsRequired();

                    b.Navigation("Marca");

                    b.Navigation("TipoVehiculo");
                });

            modelBuilder.Entity("PortalRentCar.Entities.Cliente", b =>
                {
                    b.Navigation("Alquileres");
                });

            modelBuilder.Entity("PortalRentCar.Entities.Marca", b =>
                {
                    b.Navigation("Vehiculos");
                });

            modelBuilder.Entity("PortalRentCar.Entities.TipoVehiculo", b =>
                {
                    b.Navigation("Vehiculos");
                });

            modelBuilder.Entity("PortalRentCar.Entities.Vehiculo", b =>
                {
                    b.Navigation("Alquileres");
                });
#pragma warning restore 612, 618
        }
    }
}
