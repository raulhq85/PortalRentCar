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
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Repositories.Inplementaciones
{
    public class UbicacionVehiculoRepository(PortalRentCarDbContext context) : RepositoryBase<UbicacionVehiculo>(context), IUbicacionVehiculoRepository
    {
        public async Task<UbicacionVehiculoInfo> GetAsyncUbicacionByIdVehicleAsync(int id)
        {
            try
            {
                await using var connection = Context.Database.GetDbConnection();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var entity = await connection.QueryFirstOrDefaultAsync<UbicacionVehiculoInfo>(
                    sql: "usp_ubicacion_x_id_vehiculo",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                return entity;
            }
            catch (Exception ex)
            {
                return new UbicacionVehiculoInfo();
            }
        }

        public async Task<ICollection<UbicacionVehiculoInfo>> ListAsyncUbicacionVehiculo()
        {
            try
            {
                await using var connection = Context.Database.GetDbConnection();

                var collection = await connection.QueryAsync<UbicacionVehiculoInfo>(
                    sql: "usp_ubicacion_vehiculo",
                    commandType: CommandType.StoredProcedure);

                return collection.ToList();

            }
            catch (Exception ex)
            {
                return new List<UbicacionVehiculoInfo>();
            }
        }
    }
}
