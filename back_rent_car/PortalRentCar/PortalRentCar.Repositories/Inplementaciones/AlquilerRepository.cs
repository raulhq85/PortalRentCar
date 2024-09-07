using Dapper;
using Microsoft.EntityFrameworkCore;
using PortalRentCar.DataAcces;
using PortalRentCar.Entities;
using PortalRentCar.Entities.Infos;
using PortalRentCar.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Repositories.Inplementaciones
{
    public class AlquilerRepository(PortalRentCarDbContext context) : RepositoryBase<Alquiler>(context), IAlquilerRepository
    {
        public async Task<AlquilerInfo> GetDocumentAlquilerById(int Id)
        {

            try
            {
                await using var connection = Context.Database.GetDbConnection();

                var collection = await connection.QueryAsync<AlquilerInfo>(
                    sql: "usp_get_alquiler_x_id",
                    commandType: CommandType.StoredProcedure,
                    param : new
                        {
                            Id
                        }
                    );

                return collection.FirstOrDefault();

            }
            catch (Exception ex)
            {
                return new AlquilerInfo();
            }

        }

        public async Task<Alquiler?> GetUltimoAlquilerAsync()
        {
            return await context.Set<Alquiler>()
            .OrderByDescending(v => v.Id)
            .FirstOrDefaultAsync();
        }

        public async Task<(ICollection<AlquilerInfo> Collection, int Total)> ListarAlquileresAsync(string? NroAlquiler, string? Vehiculo, int? ClienteId, string? Placa, int? TipoVehiculoId, int? MarcaId, decimal? PrecioMinimo, decimal? PrecioMaximo, int pagina, int filas)
        {
                var tupla = await ListAsync(predicado: p =>
                    (Vehiculo == null || p.Vehiculo.Nombre.Contains(Vehiculo)) &&
                    //(Cliente == null || p.Cliente.RazonSocial.Contains(Cliente)) &&
                    (ClienteId == null || p.ClienteId == ClienteId) &&
                    (NroAlquiler == null || p.NroAlquiler.Contains(NroAlquiler)) &&
                    (Placa == null || p.Vehiculo.Placa.Contains(Placa)) &&
                    (TipoVehiculoId == null || p.Vehiculo.TipoVehiculo.Id == TipoVehiculoId) &&
                    (MarcaId == null || p.Vehiculo.MarcaId == MarcaId) &&
                    (PrecioMinimo == null || p.PrecioTotal >= PrecioMinimo) &&
                    (PrecioMaximo == null || p.PrecioTotal <= PrecioMaximo),
                selector: p => new AlquilerInfo()
                {
                    Id = p.Id,
                    Nombre = p.Vehiculo.Nombre,
                    NroAlquiler = p.NroAlquiler,
                    Fecha = p.FechaCreacion,
                    Cliente = p.Cliente.RazonSocial,
                    TipoVehiculo = p.Vehiculo.TipoVehiculo.Nombre,
                    Placa = p.Vehiculo.Placa,
                    CantidadDias = EF.Functions.DateDiffDay(p.FechaInicio, p.FechaFin) + 1,
                    FechaInicio = p.FechaInicio,
                    FechaFin = p.FechaFin,
                    PrecioTotal = p.PrecioTotal,
                    Marca = p.Vehiculo.Marca.Nombre,
                    PrecioDia = p.PrecioDia
                },
                orderBy: x => x.Id,
                relaciones: "Cliente,Vehiculo,Vehiculo.TipoVehiculo,Vehiculo.Marca",
                pagina, filas);

                return tupla;
        }
    }
}
