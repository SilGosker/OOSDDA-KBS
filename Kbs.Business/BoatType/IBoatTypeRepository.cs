namespace Kbs.Business.BoatType;

public interface IBoatTypeRepository
{
    List<BoatTypeEntity> GetByBoatName(string name);
    BoatTypeEntity GetByBoatTypeID(int id);
    void Update(BoatTypeEntity boatType);
    void Create(BoatTypeEntity boatType);
    List<BoatTypeEntity> GetAll();
    BoatTypeEntity GetByReservationId(int reservationId);
    BoatTypeEntity GetById(int boatTypeId);
    List<BoatTypeEntity> GetAllWithBoats();
    void Delete(BoatTypeEntity boatType);
    BoatTypeEntity GetByBoatId(int boatId);
}