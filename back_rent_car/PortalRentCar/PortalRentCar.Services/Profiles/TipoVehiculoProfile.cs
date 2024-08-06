using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PortalRentCar.Entities;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;

namespace PortalRentCar.Services.Profiles
{
    public class TipoVehiculoProfile : Profile
    {
        public TipoVehiculoProfile()
        {
            CreateMap<TipoVehiculo, TipoVehiculoDtoResponse>();
            CreateMap<TipoVehiculo, TipoVehiculoDtoRequest>()
                .ReverseMap();
        }
    }
}
