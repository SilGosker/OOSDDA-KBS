
namespace Kbs.Business.Reservation;

public interface IReservationRepository
{
    public void Create(ReservationEntity reservation);
    public void Update(ReservationEntity reservation);
    public void Delete(ReservationEntity reservation);
    public ReservationEntity GetById(int id);

    public List<ReservationEntity> Get();
    public List<ReservationEntity> OrderByStatus(List<ReservationEntity> reservations);
    List<ReservationEntity> GetByBoatId(int boatBoatId);
        public List<ReservationEntity> GetByBoatIDAndDay(BoatEntity boat, DateTime day);
        public List<ReservationEntity> Get() 
        public List<ReservationEntity> GetByUserId (int userId) 
}