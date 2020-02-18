using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesApi.Core.Models;
using VehiclesApi.Core.Repositories;

namespace VehiclesApi.Persistence.Repositories
{
    public class VehicleOwnersRepository : IVehicleOwnersRepository
    {
        private IVehicleDbContext _context;
        public VehicleOwnersRepository(IVehicleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleOwners>> GetVehicleOwnersAsync()
        {
            return await _context.VehicleOwners.ToListAsync();
        }

        
    }
}
