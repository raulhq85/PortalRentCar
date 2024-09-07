using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalRentCar.DataAcces.Migrations.PortalRentCarDb
{
    /// <inheritdoc />
    public partial class SP_UBICACION_X_ID_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE usp_ubicacion_x_id_vehiculo( @Id INT)
                                    AS
                                    BEGIN
                                    SELECT
									ub.Id,
                                    ve.Nombre as nombre,
                                    ub.Latitud AS Latitud,
                                    ub.Longitud AS Longitud,
                                    ub.FechaPosteo as FechaPosteo,
                                    ti.Nombre as TipoVehiculo,
                                    ma.Nombre as Marca,
                                    ve.Color as Color,
                                    ve.Anio as Anio,
                                    ve.Placa as Placa,
                                    ve.Kilometraje as Kilometraje,
                                    ve.Precio as Precio,
                                    ve.ImagenUrL as ImagenUrL,
                                    cl.RazonSocial as Cliente,
                                    al.NroAlquiler as NroAlquiler
                                    FROM Vehiculo ve
                                    INNER JOIN Marca ma on ma.Id = ve.MarcaId
                                    INNER JOIN TipoVehiculo ti on ti.Id = ve.TipoVehiculoId
                                    INNER JOIN Alquiler al on al.VehiculoId = ve.Id
                                    INNER JOIN UbicacionVehiculo ub on ub.VehiculoId = al.VehiculoId
                                    INNER JOIN Cliente cl on cl.Id = al.ClienteId
									WHERE ve.Id = @Id AND ub.Estado = 1;
                                    END
                                    GO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC usp_ubicacion_x_id_vehiculo");
        }
    }
}
