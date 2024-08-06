using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalRentCar.DataAcces.Migrations.PortalRentCarDb
{
    /// <inheritdoc />
    public partial class MigracionDatosAuditoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Vehiculo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioModifica",
                table: "Vehiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Vehiculo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "UbicacionVehiculo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioModifica",
                table: "UbicacionVehiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "TipoVehiculo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioModifica",
                table: "TipoVehiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Marca",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioModifica",
                table: "Marca",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Cliente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioModifica",
                table: "Cliente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Alquiler",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioModifica",
                table: "Alquiler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TipoVehiculo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaModificacion", "IdUsuarioModifica" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.UpdateData(
                table: "TipoVehiculo",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaModificacion", "IdUsuarioModifica" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.UpdateData(
                table: "TipoVehiculo",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaModificacion", "IdUsuarioModifica" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "IdUsuarioModifica",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "UbicacionVehiculo");

            migrationBuilder.DropColumn(
                name: "IdUsuarioModifica",
                table: "UbicacionVehiculo");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "TipoVehiculo");

            migrationBuilder.DropColumn(
                name: "IdUsuarioModifica",
                table: "TipoVehiculo");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Marca");

            migrationBuilder.DropColumn(
                name: "IdUsuarioModifica",
                table: "Marca");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "IdUsuarioModifica",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Alquiler");

            migrationBuilder.DropColumn(
                name: "IdUsuarioModifica",
                table: "Alquiler");
        }
    }
}
