using Microsoft.EntityFrameworkCore;
using PortalRentCar.DataAcces;
using PortalRentCar.Entities;
using PortalRentCar.Entities.Infos;
using PortalRentCar.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Repositories.Inplementaciones
{
    public class AlquilerRepository(PortalRentCarDbContext context) : RepositoryBase<Alquiler>(context), IAlquilerRepository
    {
        public async Task<Alquiler?> GetUltimoAlquilerAsync()
        {
            return await context.Set<Alquiler>()
            .OrderByDescending(v => v.Id)
            .FirstOrDefaultAsync();
        }

        public async Task<(ICollection<AlquilerInfo> Collection, int Total)> ListarAlquileresAsync(int? ClienteId, string? Placa, int? TipoVehiculoId, int? MarcaId, decimal? PrecioMinimo, decimal? PrecioMaximo, int pagina, int filas)
        {
            var tupla = await ListAsync(predicado: p =>
                    (ClienteId == null || p.ClienteId == ClienteId) &&
                    (Placa == null || p.Vehiculo.Placa.Contains(Placa)) &&
                    (TipoVehiculoId == null || p.Vehiculo.TipoVehiculoId == TipoVehiculoId) &&
                    (MarcaId == null || p.Vehiculo.MarcaId == MarcaId) &&
                    (PrecioMinimo == null || p.PrecioTotal >= PrecioMinimo) &&
                    (PrecioMaximo == null || p.PrecioTotal <= PrecioMaximo),
                selector: p => new AlquilerInfo()
                {
                    Id = p.Id,
                    NroAlquiler = p.NroAlquiler,
                    Fecha = p.FechaCreacion,
                    Cliente = p.Cliente.RazonSocial,
                    TipoVehiculo = p.Vehiculo.TipoVehiculo.Nombre,
                    Placa = p.Vehiculo.Placa,
                    CantidadDias = EF.Functions.DateDiffDay(p.FechaInicio,p.FechaFin),
                    FechaInicio = p.FechaInicio,
                    FechaFin = p.FechaFin,
                    PrecioTotal = p.PrecioTotal
                },
                orderBy: x => x.Id,
                relaciones: "Cliente,Vehiculo.TipoVehiculo",
                pagina, filas);

            return tupla;

        }
    }
}
