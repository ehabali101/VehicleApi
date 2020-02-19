using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesApi.Core.Models;
using VehiclesApi.Persistence;

namespace VehiclesApi.Core.Repositories
{
    public interface IVehicleOwnersRepository
    {
        Task<IEnumerable<VehicleOwners>> GetVehicleOwnersAsync();
    }
}
