using PortalRentCar.Entities;
using PortalRentCar.Entities.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Repositories.Interfaces
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Task<ICollection<Cliente>> ListClienteAsync();

        Task<(ICollection<ClienteInfo> Collection, int Total)> ListClienteByParametersAsync(string? Cliente, int? ClienteId, string? NroDocumento, int pagina, int filas);
        
        Task<Cliente?> GetClienteByEmailAsync(string email);
    }
}
