namespace Kbs.Business.BoatType;

public interface IBoatTypeRepository
{
    List<BoatTypeEntity> GetAll();
    BoatTypeEntity GetByReservationId(int reservationId);
    void Create(BoatTypeEntity boatType);
    public List<BoatTypeEntity> GetName();
    public void Delete(BoatTypeEntity boatType);

}