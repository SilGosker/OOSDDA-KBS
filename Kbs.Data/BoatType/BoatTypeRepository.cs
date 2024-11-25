using Dapper;
using Kbs.Business.BoatType;
using Kbs.Business.Helpers;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.BoatType;

public class BoatTypeRepository : IBoatTypeRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);

    public List<BoatTypeEntity> GetAll()
    {
        return _connection.Query<BoatTypeEntity>("SELECT * FROM boatType").ToList();
    }
    public void Delete(BoatTypeEntity boatType)
    {
        ThrowHelper.ThrowIfNull(boatType);
        _connection.Execute("DELETE FROM Boat WHERE BoatTypeID = @BoatTypeID", boatType);
        _connection.Execute("DELETE FROM boatType WHERE BoatTypeID = @BoatTypeID", boatType);
        
    }
   
    public BoatTypeEntity GetByReservationId(int reservationID)
    {
        const string query = @"
        SELECT bt.* 
        FROM boatType bt
        INNER JOIN Boat b ON bt.BoatTypeID = b.BoatTypeID
        INNER JOIN Reservation r ON r.BoatID = b.BoatID
        WHERE r.ReservationID = @ReservationID";

        return _connection.Query<BoatTypeEntity>(query, new { ReservationID = reservationID }).FirstOrDefault();
    }

    public void Create(BoatTypeEntity boatType)
    {
        boatType.BoatTypeID = _connection.Execute(
            "INSERT INTO boatType (Name, HasSteeringWheel, RequiredExperience, Seats, Speed) VALUES (@Name, @HasSteeringWheel, @RequiredExperience, @Seats, @Speed); SELECT SCOPE_IDENTITY()",
            boatType);
    }
    
    public List<BoatTypeEntity> GetName()
    {
        return _connection.Query<BoatTypeEntity>("SELECT * FROM boatType").ToList();
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
}