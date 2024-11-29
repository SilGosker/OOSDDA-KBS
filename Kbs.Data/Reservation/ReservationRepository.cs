using Dapper;
using Kbs.Business.Boat;
using Kbs.Business.Reservation;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.Reservation;

public class ReservationRepository : IReservationRepository, IDisposable
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);

    public void Create(ReservationEntity res)
    {
        res.ReservationId =
            _connection.QueryFirst<int>(
                @"INSERT INTO Reservation (UserID, BoatID, StartTime, Length, Status)
                    VALUES (@UserID, @BoatID, @StartTime, @Length, @Status);
                    SELECT SCOPE_IDENTITY()",
                res);
    }

    public void Update(ReservationEntity res)
    {
        if (res.ReservationId == 0) return;
        _connection.Execute("UPDATE FROM Reservation WHERE ReservationID = @ReservationId", res);
    }

    public void Delete(ReservationEntity res)
    {
        if (res.ReservationId == 0) return;

        _connection.Execute("DELETE FROM Reservation WHERE ReservationID = @ReservationId", res);
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }

    public List<ReservationEntity> Get()
    {
        return _connection.Query<ReservationEntity>("SELECT * FROM Reservation").ToList();
    }

    public List<ReservationEntity> GetByBoatIdAndDay(BoatEntity boat, DateTime day)
    {
        return _connection.Query<ReservationEntity>("SELECT ReservationID, UserID, BoatID, StartTime, Length, Status  FROM Reservation WHERE BoatID = @boatId AND StartTime Like @day AND Status = 3  ORDER BY StartTime", new { day = $"%" + day.Year + "-" + day.Month + "-" + day.Day + "%", boat.BoatId }).ToList();
    }

    public List<ReservationEntity> GetByUserId(int userId)
    {
        return _connection.Query<ReservationEntity>("SELECT * FROM Reservation WHERE UserID = @userId", new { userId })
            .ToList();
    }

    public ReservationEntity GetById(int id)
    {
        return _connection.Query<ReservationEntity>("SELECT * FROM Reservation WHERE ReservationID = @id", new { id })
            .FirstOrDefault();
    }

    public List<ReservationEntity> OrderByStatusAndTime(List<ReservationEntity> reservations)
    {
        return reservations
            .OrderByDescending(r => r.Status)
            .ThenBy(r => r.StartTime)
            .ToList();
    }

    public List<ReservationEntity> GetByBoatId(int boatBoatId)
    {
        return _connection
            .Query<ReservationEntity>("SELECT * FROM Reservation WHERE BoatID = @boatBoatId", new { boatBoatId })
            .ToList();
    }
}