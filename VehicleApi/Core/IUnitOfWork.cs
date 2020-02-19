using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesApi.Core.Repositories;

namespace VehiclesApi.Core
{
    public interface IUnitOfWork
    {
        Task EnsureCreatedAsync();

        IVehicleRepository Vehicles { get; }

        IVehicleOwnersRepository VehicleOwners { get; }

        Task Complete();
    }
}
