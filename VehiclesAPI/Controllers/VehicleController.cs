using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehiclesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private IVehicle _vehicleService;

        public VehicleController(IVehicle vehicleService)
        {
            _vehicleService = vehicleService;
        }
        // GET: api/<VehicleController>
        [HttpGet]
        public async Task<IEnumerable<Vehicles>> Get()
        {
            var vehilce = await _vehicleService.GetAll();
            return vehilce;
        }

        // GET api/<VehicleController>/5
        [HttpGet("{id}")]
        public async Task<Vehicles> Get(int id)
        {
            return await _vehicleService.GetById(id);
        }

        // POST api/<VehicleController>
        [HttpPost]
        public async Task Post([FromBody]Vehicles vehicles)
        {
            await _vehicleService.addVehicle(vehicles);
        }

        // PUT api/<VehicleController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Vehicles vehicles)
        {
            await _vehicleService.updateVehicle(id, vehicles);
        }

        // DELETE api/<VehicleController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _vehicleService.deleteVehicle(id);
        }
    }
}
