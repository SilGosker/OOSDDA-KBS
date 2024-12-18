using Kbs.Business.Boat;
using Kbs.Business.Reservation;

namespace Kbs.Business.Mock
{
    public class MockReservationRepository : IReservationRepository
    {
        public List<ReservationEntity> Reservations { get; } = new();
        public void Create(ReservationEntity reservation)
        {
            Reservations.Add(reservation);
        }

        public void Delete(ReservationEntity reservation)
        {
            Reservations.Remove(reservation);
        }

        public List<ReservationEntity> Get()
        {
            return Reservations;    
        }

        public List<ReservationEntity> GetByBoatId(int boatBoatId)
        {
            return Reservations.Where(e => e.BoatId == boatBoatId).ToList();
        }

        public List<ReservationEntity> GetByBoatIdAndDay(BoatEntity boat, DateTime day)
        {
            return Reservations.Where(e => e.StartTime.Date == day.Date).ToList();
        }

        public ReservationEntity GetById(int id)
        {
            return Reservations.FirstOrDefault(e => e.ReservationId == id);
        }

        public List<ReservationEntity> GetByUserId(int userId)
        {
            return Reservations.Where(e => e.UserId == userId).ToList();
        }

        public int CountByUser(int userid)
        {
            throw new NotImplementedException();
        }

        public List<ReservationEntity> GetManyByGameId(int gameId)
        {
            throw new NotImplementedException();
        }

        public void UpdateWhenMaintained(int boatId, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public void UpdateWhenBroken(int boatId)
        {
            throw new NotImplementedException();
        }

        public List<ReservationEntity> GetByBoatWhenUpdated(int boatId, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public List<ReservationEntity> OrderByStatusAndTime(List<ReservationEntity> reservations)
        {
            return reservations.OrderByDescending(e => e.Status).ThenBy(e => e.StartTime).ToList();
        }

        public void Update(ReservationEntity reservation)
        {
            return;
        }
    }
}
