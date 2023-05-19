using ReservationAPI.Models;

namespace ReservationAPI.Interfaces
{
    public interface IReservation
    {
        Task<List<Reservation>> GetAll();
        Task UpdateMailStatus(int id);
    }
}
