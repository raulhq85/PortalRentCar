using PortalRentCar.Entities;
using PortalRentCar.Entities.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Repositories.Interfaces
{
    public interface IAlquilerRepository : IRepositoryBase<Alquiler>
    {
        Task<Alquiler?> GetUltimoAlquilerAsync();
        Task<(ICollection<AlquilerInfo> Collection, int Total)> ListarAlquileresAsync(string? NroAlquiler, string? Vehiculo, int? ClienteId, string? Placa, int? TipoVehiculoId,int? MarcaId, decimal? PrecioMinimo , decimal? PrecioMaximo, int pagina, int filas);
        Task<AlquilerInfo> GetDocumentAlquilerById(int Id);
    }
}
