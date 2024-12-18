using Dapper;
using Kbs.Business.Boat;
using Kbs.Business.User;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Kbs.Business.Session;

namespace Kbs.Data.Boat;

public class BoatRepository : IBoatRepository
{
    private readonly SqlConnection _connection = new SqlConnection(DatabaseConstants.ConnectionString);
    public List<BoatEntity> GetMany()
    {
        return _connection.Query<BoatEntity>("SELECT * FROM Boat").ToList();
    }

    public List<BoatEntity> GetManyByType(int boatTypeId)
    {
        return _connection.Query<BoatEntity>("SELECT * FROM Boat WHERE BoatTypeId = @boatTypeId", new { boatTypeId }).ToList();
    }

    public List<BoatEntity> GetManyByName(string name)
    {
        return _connection.Query<BoatEntity>("SELECT * FROM Boat WHERE LOWER(Name) LIKE @name", new { name = $"%{name.ToLower()}%" }).ToList();
    }

    public List<BoatEntity> GetManyByNameAndType(string name, int boatTypeId)
    {
        return _connection.Query<BoatEntity>("SELECT * FROM Boat WHERE LOWER(Name) LIKE @name AND BoatTypeId = @boatTypeId", new { name = $"%{name.ToLower()}%", boatTypeId }).ToList();
    }

    public List<BoatEntity> GetAvailableByType(int boatTypeId)
    {
        return _connection.Query<BoatEntity>("SELECT * FROM Boat WHERE BoatTypeId = @boatTypeId AND Status = 1",
            new { boatTypeId }).ToList();
    }

    public BoatEntity GetById(int boatId)
    {
        return _connection.QueryFirstOrDefault<BoatEntity>("SELECT * FROM Boat WHERE BoatId = @boatId", new { boatId });
    }

    public void Update(BoatEntity boat)
    {
        _connection.Execute("UPDATE Boat SET BoattypeID = @BoatTypeId, Name = @Name, Status = @Status WHERE BoatId = @BoatId", boat);
    }

    public void UpdateDeletionStatus(BoatEntity boat)
    {
        _connection.Execute("UPDATE Boat SET DeleteRequestDate = @DeleteRequestDate, Status = @Status WHERE BoatId = @BoatId", boat);

    }
    public void DeleteById(int boatId)
    {
        _connection.Execute("DELETE FROM Damage WHERE BoatId = @boatId", new { boatId });
        _connection.Execute("DELETE FROM Boat WHERE BoatId = @boatId", new { boatId });
    }
    
    public void Create(BoatEntity boat)
    {
        boat.BoatId = _connection.QueryFirst<int>(
            "INSERT INTO Boat (Name, BoatTypeId, Status) VALUES (@Name, @BoatTypeId, 1); SELECT SCOPE_IDENTITY()",
            boat);
    }

    public List<BoatEntity> GetManyByGame(int gameId)
    {
        return _connection.Query<BoatEntity>("SELECT * FROM Boat WHERE BoatId in (SELECT BoatId FROM Reservation WHERE GameId = @GameId)", new { gameId }).ToList();
    }
    
}