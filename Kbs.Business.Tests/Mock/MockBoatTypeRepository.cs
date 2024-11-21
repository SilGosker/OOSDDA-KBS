using Kbs.Business.BoatType;

namespace Kbs.Business.Mock;

public class MockBoatTypeRepository : IBoatTypeRepository
{
    public List<BoatTypeEntity> BoatTypeEntities { get; set; } = new();
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
        throw new NotImplementedException();
    }

    public void Create(BoatTypeEntity boatType)
    {
        throw new NotImplementedException();
    }
}