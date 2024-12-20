namespace Kbs.Business.BoatType;

public interface IBoatTypeRepository
{
    void Create(BoatTypeEntity boatType);
    List<BoatTypeEntity> GetAll();
    BoatTypeEntity GetByReservationId(int reservationId);
    BoatTypeEntity GetById(int boatTypeId);
    List<BoatTypeEntity> GetAllWithBoats();
    void Update(BoatTypeEntity boatType);
    BoatTypeEntity GetByBoatId(int boatId);
}