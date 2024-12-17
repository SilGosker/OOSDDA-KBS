using Dapper;
using Kbs.Business.Boat;
using Kbs.Business.BoatType;
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
                @"INSERT INTO Reservation (UserID, BoatID, StartTime, Length, Status, GameID)
                    VALUES (@UserID, @BoatID, @StartTime, @Length, @Status, @GameId);
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
        string daystring = day.Day < 10 ? "0" + day.Day : day.Day.ToString();
        string monthstring = day.Month < 10 ? "0" + day.Month : day.Month.ToString();
        return _connection.Query<ReservationEntity>(
            @"SELECT ReservationID, UserID, BoatID, StartTime, Length, Status  
                FROM Reservation 
                WHERE BoatID = @boatId 
                  AND StartTime Like @day 
                  AND Status = 3 
                ORDER BY StartTime",
            new { day = "%" + day.Year + "-" + monthstring + "-" + daystring + "%", boat.BoatId }).ToList();
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

    public int CountByUser(int userId)
    {
        return _connection.QuerySingleOrDefault<int>(
            "SELECT COUNT(ReservationID) FROM Reservation WHERE UserID = @userId",
            new { userId }
        );
    }

    public async Task ChangeStatusAsync()
    {
        await _connection.ExecuteAsync(
            "UPDATE Reservation\r\nSET Status = 2\r\nWHERE DATEADD(MINUTE, DATEDIFF(MINUTE, '00:00:00', Length), StartTime) < GETDATE() AND Status = 3;");
    }

    public List<ReservationEntity> GetManyByGameId(int gameId)
    {
        string sql = @"
        SELECT 
            r.*,
            b.BoatID AS BoatId,
            b.Name
        FROM 
            Reservation r
        JOIN 
            Boat b ON r.BoatID = b.BoatID
        WHERE 
            r.GameID = @gameId";

        return _connection.Query<ReservationEntity, BoatEntity, ReservationEntity>(
            sql,
            (reservation, boat) =>
            {
                reservation.Boat = boat;
                return reservation;
            },
            new { gameId },
            splitOn: "BoatId"
        ).ToList();
    }
    public List<ReservationEntity> GetReservationsWithActiveBoatsByUserId(int userId)
    {
        var query = @"
        SELECT r.*
        FROM Reservation r
        INNER JOIN Boat b ON r.BoatId = b.BoatId
        WHERE r.UserId = @UserId
          AND b.Status = 1 OR b.Status = 2";

        return _connection.Query<ReservationEntity>(query, new { UserId = userId }).ToList();
    }
    public void UpdateWhenBroken(int boatId)
    {
        var query = @"UPDATE Reservation 
                  SET Status = 1 
                  WHERE BoatID = @BoatId";

        _connection.Execute(query, new { BoatId = boatId });
    }
    public void UpdateWhenMaintained(int boatId, DateTime endDate)
    {
        endDate = endDate.Date;
        var query = @"UPDATE Reservation 
                  SET Status = 1 
                  WHERE BoatID = @BoatId AND CONVERT(DATE, StartTime) < @EndDate AND StartTime IS NOT NULL";

        _connection.Execute(query, new { BoatId = boatId, EndDate = endDate });
    }
}