using Dapper;
using Kbs.Business.BoatType;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.BoatType;

public class BoatTypeRepository : IBoatTypeRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);
    public List<BoatTypeEntity> GetAll()
    {
        return _connection.Query<BoatTypeEntity>("SELECT * FROM Boattype").ToList();
    }

    public void Create(BoatTypeEntity boatType)
    {
        boatType.BoatTypeID = _connection.Execute(
            "INSERT INTO Boattype (Name, HasSteeringWheel, RequiredExperience, Seats, Speed) VALUES (@Name, @HasSteeringWheel, @RequiredExperience, @Seats, @Speed); SELECT SCOPE_IDENTITY()",
            boatType);
    }
}