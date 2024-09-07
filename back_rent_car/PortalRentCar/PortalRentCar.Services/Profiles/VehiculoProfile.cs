using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PortalRentCar.Entities;
using PortalRentCar.Entities.Infos;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Profiles
{
    public class VehiculoProfile : Profile
    {
        public VehiculoProfile()
        {
            CreateMap<Vehiculo, VehiculoDtoResponse>();

            CreateMap<VehiculoInfo, VehiculoDtoResponse>();

            CreateMap<VehiculoDtoRequest, Vehiculo>()
                .ReverseMap();

            CreateMap<VehiculoHomeInfo, VehiculoHomeDtoResponse>();

            //CreateMap<UbicacionVehiculoInfo, UbicacionVehiculoDtoResponse>();
        }
    }
}
