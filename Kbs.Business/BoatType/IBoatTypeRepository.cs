namespace Kbs.Business.BoatType;

public interface IBoatTypeRepository
{
    List<BoatTypeEntity> GetAll();
    void Create(BoatTypeEntity boatType);
}