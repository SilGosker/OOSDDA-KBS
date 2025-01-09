using Dapper;
using Kbs.Business.BoatType;
using Kbs.Business.Helpers;
using Kbs.Business.User;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.BoatType;

public class BoatTypeRepository : IBoatTypeRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);

    public List<BoatTypeEntity> GetAll()
    {
        return _connection.Query<BoatTypeEntity>("SELECT * FROM Boattype").ToList();
    }
    public void Delete(BoatTypeEntity boatType)
    {
        ThrowHelper.ThrowIfNull(boatType);
        _connection.Execute("DELETE FROM Damage WHERE (BoatID IN (SELECT BoatID FROM Boat WHERE BoatTypeID = @BoatTypeID))", boatType);
        _connection.Execute("DELETE FROM Reservation WHERE BoatID IN (SELECT BoatID FROM Boat WHERE BoatTypeID = @BoatTypeID)", boatType);
        _connection.Execute("DELETE FROM Boat WHERE BoatTypeID = @BoatTypeID", boatType);
        _connection.Execute("DELETE FROM boatType WHERE BoatTypeID = @BoatTypeID", boatType);
    }
   
    public BoatTypeEntity GetByReservationId(int reservationID)
    {
        const string query = @"
        SELECT bt.* 
        FROM Boattype bt
        INNER JOIN Boat b ON bt.BoattypeID = b.BoattypeID
        INNER JOIN Reservation r ON r.BoatID = b.BoatID
        WHERE r.ReservationID = @ReservationID";

        return _connection.Query<BoatTypeEntity>(query, new { ReservationID = reservationID }).FirstOrDefault();
    }
    

    public BoatTypeEntity GetById(int boatTypeId)
    {
        return _connection.QueryFirstOrDefault<BoatTypeEntity>("SELECT * FROM Boattype WHERE BoattypeID = @boatTypeId",
            new { boatTypeId });
    }
    
    public void Create(BoatTypeEntity boatType)
    {
        boatType.BoatTypeId = _connection.QueryFirst<int>(
            "INSERT INTO Boattype (Name, HasSteeringWheel, RequiredExperience, Seats, Speed) VALUES (@Name, @HasSteeringWheel, @RequiredExperience, @Seats, @Speed); SELECT SCOPE_IDENTITY()",
            boatType);
    }

    public void Update(BoatTypeEntity boatType)
    {
      _connection.Execute(
            "UPDATE Boattype SET Name = @Name, HasSteeringWheel = @HasSteeringWheel, RequiredExperience = @RequiredExperience, Seats = @Seats, Speed = @Speed WHERE BoattypeID = @BoatTypeId",
            boatType);
    }

    public BoatTypeEntity GetByBoatTypeID(int id)
    {
        const string query = @"SELECT * FROM boatType WHERE BoatTypeID = @BoatTypeID";
        return _connection.Query<BoatTypeEntity>(query, new { BoatTypeID = id }).FirstOrDefault();
    }
    public List<BoatTypeEntity> GetByBoatName(string name)
    {
        const string query = @"SELECT * FROM boatType WHERE Name = @Name";
        return _connection.Query<BoatTypeEntity>(query, new { Name = name }).ToList();

    }

    public List<BoatTypeEntity> GetAllWithBoats()
    {
        return _connection
            .Query<BoatTypeEntity>(@"SELECT * FROM Boattype WHERE BoattypeID IN (SELECT BoatTypeId FROM Boat WHERE Status = 1)")
            .ToList();
    }

    public BoatTypeEntity GetByBoatId(int boatId)
    {
        const string query = @"
        SELECT bt.* 
        FROM Boattype bt
        INNER JOIN Boat b ON bt.BoattypeID = b.BoattypeID
        WHERE b.BoatID = @boatId";

        return _connection.Query<BoatTypeEntity>(query, new { boatId }).FirstOrDefault();
    }
}