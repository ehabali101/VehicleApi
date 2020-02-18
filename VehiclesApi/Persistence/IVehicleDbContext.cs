using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesApi.Core.Models;

namespace VehiclesApi.Persistence
{
    public interface IVehicleDbContext
    {
        DbSet<Vehicle> Vehicles { get; set; }
        DbSet<VehicleOwners> VehicleOwners { get; set; }
    }
}
