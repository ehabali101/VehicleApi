using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesApi.Core.Models;
using VehiclesApi.Core.Repositories;

namespace VehiclesApi.Persistence.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private IVehicleDbContext _context;
        public VehicleRepository(IVehicleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetVehicleAsync(string id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task UpdateVehicleStatus(string id, Vehicle vehicle)
        {
            var dbVehicle = await _context.Vehicles.FindAsync(id);
            dbVehicle.Status = vehicle.Status;         
        }

        public bool VehicleExists(string id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
