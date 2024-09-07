using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalRentCar.DataAcces.Migrations.PortalRentCarDb
{
    /// <inheritdoc />
    public partial class SP_CLIENTE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE usp_listar_cliente
(
	@Cliente NVARCHAR(200) = NULL,
    @ClienteId INT = NULL,
	@NroDocumento NVARCHAR(200) = NULL,
    @Pagina INT = 0,
    @Filas INT = 5
)
AS
BEGIN

SET @Pagina = @Pagina * @Filas;

	SELECT
	   c.Id,
	   c.NroDocumento,
	   c.RazonSocial,
	   c.Correo,
	   c.Telefono
	FROM Cliente c
	where c.Estado = 1
	AND (@ClienteId IS NULL OR c.Id = @ClienteId)
	AND (@NroDocumento IS NULL OR c.NroDocumento = @NroDocumento)
	AND (@Cliente IS NULL OR c.RazonSocial LIKE '%' + @Cliente + '%')
    ORDER BY c.Id -- Make sure to have an ORDER BY clause
    OFFSET @Pagina ROWS FETCH NEXT @Filas ROWS ONLY;

	SELECT
	  count(1) total
	FROM Cliente c
	where c.Estado = 1
	AND (@ClienteId IS NULL OR c.Id = @ClienteId)
	AND (@NroDocumento IS NULL OR c.NroDocumento = @NroDocumento)
	AND (@Cliente IS NULL OR c.RazonSocial LIKE '%' + @Cliente + '%')
END
GO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC usp_listar_cliente");
        }
    }
}
