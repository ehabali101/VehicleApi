using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesApi.Core;
using VehiclesApi.Core.Models;
using VehiclesApi.Core.Repositories;
using VehiclesApi.Persistence.Repositories;

namespace VehiclesApi.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private VehicleDbContext _context;
        public IVehicleRepository Vehicles { get; private set; }
        public IVehicleOwnersRepository VehicleOwners { get; private set; }

        public UnitOfWork(VehicleDbContext context)
        {
            _context = context;

            Vehicles = new VehicleRepository(_context);
            VehicleOwners = new VehicleOwnersRepository(_context);
        }

        public async Task EnsureCreatedAsync()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            await WriteVehiclesAsync();
        }

        private async Task WriteVehiclesAsync()
        {
            // to add vehicles to database
            var vehicles = new List<Vehicle>()
            {
                new Vehicle{ Id= "YS2R4X20005399401", Status = VehicleStatus.Disconnected },
                new Vehicle{ Id= "VLUR4X20009093588", Status = VehicleStatus.Disconnected },
                new Vehicle{ Id= "VLUR4X20009048066", Status = VehicleStatus.Connected },
                new Vehicle{ Id= "YS2R4X20005388011", Status = VehicleStatus.Connected },
                new Vehicle{ Id= "YS2R4X20005387949", Status = VehicleStatus.Disconnected },
                new Vehicle{ Id= "YS2R4X20005387055", Status = VehicleStatus.Connected }
            };
            _context.Vehicles.AddRange(vehicles);

            // to add vehicles to Vehicle Owners
            var owners = new List<VehicleOwners>()
            {
                new VehicleOwners{ Id = "001", CustomerId = new Guid("e5e133d6-b245-4ced-bde3-e047888de611"), VehicleId = "YS2R4X20005399401", RegistrationNumber = "ABC123" },
                new VehicleOwners{ Id = "002", CustomerId = new Guid("e5e133d6-b245-4ced-bde3-e047888de611"), VehicleId = "VLUR4X20009093588", RegistrationNumber = "DEF456" },
                new VehicleOwners{ Id = "003", CustomerId = new Guid("e5e133d6-b245-4ced-bde3-e047888de611"), VehicleId = "VLUR4X20009048066", RegistrationNumber = "GHI789" },
                new VehicleOwners{ Id = "004", CustomerId = new Guid("91725592-44ff-4b58-ad30-5fd1e6933a42"), VehicleId = "YS2R4X20005388011", RegistrationNumber = "JKL012" },
                new VehicleOwners{ Id = "005", CustomerId = new Guid("91725592-44ff-4b58-ad30-5fd1e6933a42"), VehicleId = "YS2R4X20005387949", RegistrationNumber = "MNO345" },
                new VehicleOwners{ Id = "006", CustomerId = new Guid("3679d5bf-5314-4b47-8187-373151dc22ea"), VehicleId = "VLUR4X20009048066", RegistrationNumber = "PQR678" },
                new VehicleOwners{ Id = "007", CustomerId = new Guid("3679d5bf-5314-4b47-8187-373151dc22ea"), VehicleId = "YS2R4X20005387055", RegistrationNumber = "STU901" },
            };
            _context.VehicleOwners.AddRange(owners);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateVehicle(Vehicle vehicle)
        {
            var item = await _context.Vehicles.Where(i => i.Id.Equals(vehicle.Id)).FirstOrDefaultAsync();
            item.Status = vehicle.Status;
            _context.Update(item);
            await _context.SaveChangesAsync(true);
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
       
    }
}
