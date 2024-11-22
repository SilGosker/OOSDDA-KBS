namespace Kbs.Business.BoatType;

public interface IBoatTypeRepository
{
    List<BoatTypeEntity> GetAll();
    BoatTypeEntity GetByReservationId(int reservationId);
    BoatTypeEntity GetById(int boatTypeId);
    void Create(BoatTypeEntity boatType);
    List<BoatTypeEntity> GetAllWithBoats();
}