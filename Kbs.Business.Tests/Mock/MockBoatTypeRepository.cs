using Kbs.Business.BoatType;

namespace Kbs.Business.Mock;

public class MockBoatTypeRepository : IBoatTypeRepository
{

    public List<BoatTypeEntity> BoatTypeEntities = new()
    {
        new() { BoatTypeId = 1, Name = "Sailboat" }
    };
    public List<BoatTypeEntity> GetAll()
    {
        return BoatTypeEntities;
    }

    public BoatTypeEntity GetByReservationId(int reservationId)
    {
        throw new NotImplementedException();
    }

    public BoatTypeEntity GetById(int boatTypeId)
    {
        return BoatTypeEntities.FirstOrDefault(x => x.BoatTypeId == boatTypeId);
    }

    public List<BoatTypeEntity> GetAllWithBoats()
    {
        throw new NotImplementedException();
    }

    public void Delete(BoatTypeEntity boatType)
    {
        throw new NotImplementedException();
    }

    public BoatTypeEntity GetByBoatId(int boatId)
    {
        throw new NotImplementedException();
    }

    public List<BoatTypeEntity> GetByBoatName(string name)
    {
        throw new NotImplementedException();
    }

    public BoatTypeEntity GetByBoatTypeID(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(BoatTypeEntity boatType)
    {
        throw new NotImplementedException();
    }

    public void Create(BoatTypeEntity boatType)
    {
        throw new NotImplementedException();
    }
}