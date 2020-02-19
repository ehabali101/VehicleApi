using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesApi.Core.Models;
using VehicleResources;

namespace VehicleApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleResource>().ReverseMap();
            CreateMap<VehicleOwners, VehicleOwnersResource>().ReverseMap();
            CreateMap<VehicleStatus, VehicleStatusResource>().ReverseMap();
        }
    }
}
