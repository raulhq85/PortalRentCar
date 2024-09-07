using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PortalRentCar.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.DataAcces
{
    public static class UserDataSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<RentCarIdentityUser>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            var adminRole = new IdentityRole(Constantes.RolAdministrador);
            var clienteRole = new IdentityRole(Constantes.RolCliente);

            if (!await roleManager.RoleExistsAsync(Constantes.RolAdministrador))
            {
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync(Constantes.RolCliente))
            {
                await roleManager.CreateAsync(clienteRole);
            }

            var adminUser = new RentCarIdentityUser()
            {
                RazonSocial = "Administrador del Sistema",
                UserName = "admin",
                Email = "hqraul85@gmail.com",
                PhoneNumber = "+1 99 999 999 999",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "$Admin.2024");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, Constantes.RolAdministrador);
            }
        }

    }
}
