namespace Kbs.Business.Boat;

public interface IBoatRepository
{
    List<BoatEntity> GetMany();
    List<BoatEntity> GetManyByType(int boatTypeId);
    List<BoatEntity> GetManyByName(string name);
    List<BoatEntity> GetManyByNameAndType(string name, int boatTypeId);
    BoatEntity GetById(int boatId);
    void Update(BoatEntity boat);
    void DeleteById(int boatId);
    void Create(BoatEntity boat);
}