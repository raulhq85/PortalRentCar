using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalRentCar.DataAcces.Migrations.PortalRentCarDb
{
    /// <inheritdoc />
    public partial class SP_PDF_ALQUILER : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE usp_get_alquiler_x_id(
                                @Id INT
                                )
                                AS
                                BEGIN
                                    SELECT
                                    al.Id as Id,
                                    ve.Nombre as Nombre,
                                    al.NroAlquiler as NroAlquiler,
                                    al.FechaCreacion as Fecha,
                                    cl.RazonSocial as Cliente,
                                    ti.Nombre as TipoVehiculo,
                                    ve.Placa as Placa,
                                    ma.Nombre as Marca,
                                    DATEDIFF(DAY,al.FechaInicio,al.FechaFin) + 1 as CantidadDias,
                                    al.FechaInicio as FechaInicio,
                                    al.FechaFin as FechaFin,
                                    al.PrecioTotal as PrecioTotal,
                                    al.PrecioDia as PrecioDia
                                    FROM Alquiler al
                                    INNER JOIN Vehiculo ve on ve.Id = al.VehiculoId
                                    INNER JOIN Marca ma on ma.Id = ve.MarcaId
                                    INNER JOIN TipoVehiculo ti on ti.Id = ve.TipoVehiculoId
                                    INNER JOIN Cliente cl on cl.Id = al.ClienteId
                                    WHERE AL.Id = @Id;
                                END
                                GO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC usp_get_alquiler_x_id");
        }
    }
}
