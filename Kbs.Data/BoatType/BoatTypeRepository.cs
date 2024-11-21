using Dapper;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.BoatType;

public class BoatTypeRepository : IBoatTypeRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);
    public List<BoatTypeEntity> GetAll()
    {
        return _connection.Query<BoatTypeEntity>("SELECT * FROM Boattype").ToList();
    }
    public void Delete(BoatTypeEntity Boattype)
    {
        //Boattype.BoatTypeID = _connection.Execute("DELETE FROM Boattype (Name, HasSteeringWheel, RequiredExperience, Seats, Speed) VALUES (@Name, @HasSteeringWheel, @RequiredExperience, @Seats, @Speed); SELECT SCOPE_IDENTITY()",  boatType);
        if (Boattype.BoatTypeID == 0) return;

        _connection.Execute("DELETE FROM Boattype WHERE Boattype = @Boattype", Boattype);
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
    public void Create(BoatTypeEntity boatType)
    {
        boatType.BoatTypeID = _connection.Execute(
            "INSERT INTO Boattype (Name, HasSteeringWheel, RequiredExperience, Seats, Speed) VALUES (@Name, @HasSteeringWheel, @RequiredExperience, @Seats, @Speed); SELECT SCOPE_IDENTITY()",
            boatType);
    }
    public List<BoatTypeEntity> GetName()
    {
        return _connection.Query<BoatTypeEntity>("SELECT * FROM Boattype").ToList();
    }
    
    public void Delete(List<BoatTypeEntity> entity)
    {
        throw new NotImplementedException();
    }
    
}