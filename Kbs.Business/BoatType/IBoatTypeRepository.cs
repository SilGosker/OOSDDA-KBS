namespace Kbs.Business.BoatType;

public interface IBoatTypeRepository
{
    List<BoatTypeEntity> GetAll();

    BoatTypeEntity GetByReservationId(int reservationId);
}