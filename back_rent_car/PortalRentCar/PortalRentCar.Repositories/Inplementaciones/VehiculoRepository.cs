using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PortalRentCar.DataAcces;
using PortalRentCar.Entities;
using PortalRentCar.Entities.Infos;
using PortalRentCar.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Repositories.Inplementaciones
{
    public class VehiculoRepository(PortalRentCarDbContext context) : RepositoryBase<Vehiculo>(context), IVehiculoRepository
    {
        public async Task<VehiculoHomeInfo?> GetVehiculoHomeByIdAsync(int id)
        {
            try
            {
                return await Context.Set<Vehiculo>()
                .Where(p => p.Id == id && p.SituacionVehiculo == 0)
                .Select(p => new VehiculoHomeInfo
                {
                    Id = p.Id,
                    Color = p.Color,
                    Anio = p.Anio,
                    ImagenUrL = p.ImagenUrL,
                    Kilometraje = p.Kilometraje,
                    Marca = p.Marca.Nombre,
                    Nombre = p.Nombre,
                    Placa =  p.Placa,
                    Precio = p.Precio,
                    TipoVehiculo = p.TipoVehiculo.Nombre
                })
                .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<ICollection<Vehiculo>> ListarAsync()
        {
            try
            {
                return await Context.Set<Vehiculo>()
                    .Include(p => p.Marca)
                    .Include(p => p.TipoVehiculo)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch(Exception ex){
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<(ICollection<VehiculoInfo> Collection, int Total)> ListarVehiculoByParametersAsync(string? Nombre, int? TipoVehiculoId, int? MarcaId, int? Anio, decimal? PrecioMinimo, decimal? PrecioMaximo,int? SituacionId, int pagina, int filas)
        {
            try
            {
                var predicate = PredicateBuilder.New<Vehiculo>(true);

                if (Nombre != null)
                    predicate = predicate.And(p => p.Nombre == Nombre);

                if (TipoVehiculoId != null)
                    predicate = predicate.And(p => p.TipoVehiculoId == TipoVehiculoId);

                if (MarcaId != null)
                    predicate = predicate.And(p => p.MarcaId == MarcaId);

                if (Anio != null)
                    predicate = predicate.And(p => p.Anio == Anio);

                if (PrecioMinimo != null)
                    predicate = predicate.And(p => p.Precio >= PrecioMinimo);

                if (PrecioMaximo != null)
                    predicate = predicate.And(p => p.Precio <= PrecioMaximo);

                if (SituacionId.HasValue)
                {
                    if (SituacionId == 3)
                    {
                        predicate = predicate.And(p => (int)p.SituacionVehiculo == 0 || (int)p.SituacionVehiculo == 1 || (int)p.SituacionVehiculo == 2);
                    }
                    else
                    {
                        predicate = predicate.And(p => (int)p.SituacionVehiculo == SituacionId);
                    }
                }

                var total = await Context.Set<Vehiculo>()
                    .Where(predicate)
                    .CountAsync();

                var tupla = await ListAsync(predicado: predicate,
                    selector: p => new VehiculoInfo()
                    {
                        Id = p.Id,
                        Color = p.Color,
                        Anio = p.Anio,
                        ImagenUrL = p.ImagenUrL,
                        Kilometraje = p.Kilometraje,
                        Marca = p.Marca.Nombre,
                        Nombre = p.Nombre,
                        Placa = p.Placa,
                        Precio = p.Precio,
                        TipoVehiculo = p.TipoVehiculo.Nombre,
                        SituacionVehiculo = p.SituacionVehiculo.ToString()
                    },
                    orderBy: x => x.Id,
                    relaciones: "Marca,TipoVehiculo",
                    pagina, filas);

                return tupla;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<(ICollection<VehiculoHomeInfo> Collection, int Total)> ListarVehiculoHomeAsync(string? Nombre, int? TipoVehiculoId, int? MarcaId, int? Anio, decimal? PrecioMinimo, decimal? PrecioMaximo, int pagina, int filas)
        {
            try
            {
                var total = await Context.Set<Vehiculo>()
                   .Where(p => (Nombre == null || p.Nombre == Nombre) &&
                               (TipoVehiculoId == null || p.TipoVehiculoId == TipoVehiculoId) &&
                               (MarcaId == null || p.MarcaId == MarcaId) &&
                               (Anio == null || p.Anio == Anio) &&
                               (PrecioMinimo == null || p.Precio >= PrecioMinimo) &&
                               (PrecioMaximo == null || p.Precio <= PrecioMaximo) &&
                               (p.SituacionVehiculo == 0))
                   .CountAsync();


                var tupla = await ListAsync(predicado: p => (Nombre == null || p.Nombre == Nombre) &&
                                                       (TipoVehiculoId == null || p.TipoVehiculoId == TipoVehiculoId) &&
                                                       (MarcaId == null || p.MarcaId == MarcaId) &&
                                                       (Anio == null || p.Anio == Anio) &&
                                                       (PrecioMinimo == null || p.Precio >= PrecioMinimo) &&
                                                       (PrecioMaximo == null || p.Precio <= PrecioMaximo)
                                                       && p.SituacionVehiculo == 0,
               selector: p => new VehiculoHomeInfo
               {
                   Id = p.Id,
                   Color = p.Color,
                   Anio = p.Anio,
                   ImagenUrL = p.ImagenUrL,
                   Kilometraje = p.Kilometraje,
                   Marca = p.Marca.Nombre,
                   Nombre = p.Nombre,
                   Placa = p.Placa,
                   Precio = p.Precio,
                   TipoVehiculo = p.TipoVehiculo.Nombre
               },
               orderBy: x => x.Id,
               relaciones: "Marca,TipoVehiculo",
               pagina: pagina,
               filas: filas);

                return tupla;
            }
            catch(Exception ex){
                Console.WriteLine(ex);
                throw;
            }
               
        }
    }
}
