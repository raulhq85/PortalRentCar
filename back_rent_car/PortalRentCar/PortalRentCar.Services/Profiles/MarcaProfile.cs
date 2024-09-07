using AutoMapper;
using PortalRentCar.Entities;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Profiles
{
    public class MarcaProfile : Profile
    {
        public MarcaProfile() 
        {
            CreateMap<Marca,MarcaDtoResponse>();
            CreateMap<Marca, MarcaDtoRequest>()
                .ReverseMap();
        }
    }
}
