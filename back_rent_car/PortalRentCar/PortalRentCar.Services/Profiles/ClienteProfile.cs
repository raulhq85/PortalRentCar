using PortalRentCar.Entities.Infos;
using PortalRentCar.Entities;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace PortalRentCar.Services.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDtoResponse>();

            CreateMap<ClienteInfo, ClienteDtoResponse>();

            CreateMap<Cliente, ClienteDtoRequest>()
                .ReverseMap();
        }
    }
}
