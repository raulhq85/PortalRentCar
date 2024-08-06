using Microsoft.EntityFrameworkCore.Infrastructure;
using PortalRentCar.Entities;
using PortalRentCar.Entities.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Repositories.Interfaces
{
    public interface IVehiculoRepository : IRepositoryBase<Vehiculo>
    {
        Task<ICollection<Vehiculo>> ListarAsync();

        Task<(ICollection<VehiculoInfo> Collection, int Total)> ListarVehiculoByParametersAsync(string? Nombre, int? TipoVehiculoId, int? MarcaId, int? Anio,decimal? PrecioMinimo, decimal? PrecioMaximo, int? Situacion, int pagina, int filas);

        Task<VehiculoHomeInfo?> GetVehiculoHomeByIdAsync(int id);

        Task<(ICollection<VehiculoHomeInfo> Collection, int Total)> ListarVehiculoHomeAsync(string? Nombre, int? TipoVehiculoId, int? MarcaId, int? Anio, decimal? PrecioMinimo, decimal? PrecioMaximo, int pagina, int filas);

    }
}
