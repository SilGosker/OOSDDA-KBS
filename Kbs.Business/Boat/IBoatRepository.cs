namespace Kbs.Business.Boat;

public interface IBoatRepository
{
    List<BoatEntity> GetMany();
    List<BoatEntity> GetManyByType(int boatTypeId);
    List<BoatEntity> GetManyByName(string name);
    List<BoatEntity> GetManyByNameAndType(string name, int boatTypeId);
    BoatEntity GetById(int boatId);
    void UpdateDeleteRequestDate(int boatId, DateTime? requestDate, BoatStatus status);
    void DeleteById(int boatId);
}