
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

    public BoatTypeEntity GetByReservationId(int reservationID)
    {
        const string values = @"
        SELECT bt.* 
        FROM Boattype bt
        INNER JOIN Boat b ON bt.BoattypeID = b.BoattypeID
        INNER JOIN Reservation r ON r.ReservationID = @ReservationID";
        

        return _connection.Query<BoatTypeEntity>(values, new { ReservationID = reservationID })
                          .FirstOrDefault();
    }
}