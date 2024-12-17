
using Kbs.Business.Boat;
using Kbs.Business.BoatType;

namespace Kbs.Business.Reservation;

public interface IReservationRepository
{
    public void Create(ReservationEntity reservation);
    public void Update(ReservationEntity reservation);
    public void Delete(ReservationEntity reservation);
    public ReservationEntity GetById(int id);
    public List<ReservationEntity> Get();
    public List<ReservationEntity> OrderByStatusAndTime(List<ReservationEntity> reservations);
    public List<ReservationEntity> GetByBoatId(int boatBoatId);
    public List<ReservationEntity> GetByBoatIdAndDay(BoatEntity boat, DateTime day);
    public List<ReservationEntity> GetByUserId(int userId);
    public int CountByUser(int userid);
    public List<ReservationEntity> GetManyByGameId(int gameId);
    public void UpdateWhenMaintained(int boatId, DateTime endDate);
    public void UpdateWhenBroken(int boatId);
}
