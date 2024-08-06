using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.DataAcces
{
    public class RentCarIdentityUser : IdentityUser
    {
        [StringLength(100)]
        public string RazonSocial { get; set; } = default!;
    }
}
