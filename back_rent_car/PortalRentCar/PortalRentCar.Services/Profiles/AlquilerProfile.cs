using AutoMapper;
using PortalRentCar.Entities.Infos;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Profiles
{
    public class AlquilerProfile : Profile
    {
        public AlquilerProfile()
        {
            CreateMap<AlquilerInfo, AlquilerDtoResponse>();
        }
    }
}
