using Microsoft.EntityFrameworkCore;
using PortalRentCar.DataAcces;
using PortalRentCar.Entities;
using PortalRentCar.Entities.Infos;
using PortalRentCar.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace PortalRentCar.Repositories.Inplementaciones
{
    public class ClienteRepository(PortalRentCarDbContext context) : RepositoryBase<Cliente>(context), IClienteRepository
    {
        public async Task<Cliente?> GetClienteByEmailAsync(string email)
        {
            return await Context.Set<Cliente>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Correo == email);
        }

        public async Task<ICollection<Cliente>> ListClienteAsync()
        {
            return await Context.Set<Cliente>()
               .Include(p => p.Alquileres)
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<(ICollection<ClienteInfo> Collection, int Total)> ListClienteByParametersAsync(string? Cliente, int? ClienteId, string? NroDocumento, int pagina, int filas)
        {

                    try
                    {
                       await using var multipleQuery = await Context.Database.GetDbConnection()
                       .QueryMultipleAsync(
                       sql: "usp_listar_cliente",
                       commandType: CommandType.StoredProcedure,
                       param: new
                       {
                           Cliente,
                           ClienteId = ClienteId == 0 ? (int?)null : ClienteId,
                           NroDocumento,
                           pagina = pagina - 1,
                           filas
                       });

                        var collection = multipleQuery.Read<ClienteInfo>().ToList();
                        var total = multipleQuery.ReadFirst<int>();

                        return (collection, total);
                    }
                    catch (Exception ex)
                    {
                        return (new List<ClienteInfo>(), 0);
                    }
        }
    }
}
