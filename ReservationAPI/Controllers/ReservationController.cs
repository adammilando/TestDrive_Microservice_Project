using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservation _reservationService;
        public ReservationController(IReservation reservation)
        {
            _reservationService = reservation;
        }
        // GET: api/<ReservationController>
        [HttpGet]
        public async Task<IEnumerable<Reservation>> Get()
        {
            var reservation = await _reservationService.GetAll();
            return reservation;
        }


        // PUT api/<ReservationController>/5
        [HttpPut("{id}")]
        public async Task Put(int id)
        {
            await _reservationService.UpdateMailStatus(id);

        }

    }
}
