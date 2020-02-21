using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleResources;
using VehiclesApi.Core;
using VehiclesApi.Core.Models;
using VehiclesApi.Persistence;

namespace VehiclesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehiclesController(IUnitOfWork unitOfwork, IMapper mapper)
        {
            _unitOfWork = unitOfwork;
            _mapper = mapper;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleResource>>> GetVehicles()
        {
            // to craete database
            //await _unitOfWork.EnsureCreatedAsync();

            var vehicles = await _unitOfWork.Vehicles.GetVehiclesAsync();
            return Ok(_mapper.Map<List<VehicleResource>>(vehicles));
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleResource>> GetVehicle(string id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetVehicleAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VehicleResource>(vehicle));
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutVehicle(string id, [FromBody]VehicleResource vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            await _unitOfWork.UpdateVehicle(_mapper.Map<Vehicle>(vehicle));
            //await _unitOfWork.Vehicles.UpdateVehicleStatus(id, _mapper.Map<Vehicle>(vehicle));
            //await _unitOfWork.Complete();

            //return Ok(_mapper.Map< VehicleResource>(vehicle));
            return Ok();
        }

        // GET: api/Vehicles/Owners
        [HttpGet]
        [Route("Owners")]
        public async Task<ActionResult<IEnumerable<VehicleOwnersResource>>> GetVehicleOwners()
        {
            var owners = await _unitOfWork.VehicleOwners.GetVehicleOwnersAsync();
            return Ok(_mapper.Map<List<VehicleOwnersResource>>(owners));
        }

    }
}
