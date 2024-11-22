using Dapper;
using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Reservation;
using Kbs.Business.User;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbs.Data.Reservation
{
    public class ReservationRepository : IReservationRepository, IDisposable
    {
        private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);

        public void Create(ReservationEntity res)
        {
            res.ReservationId = _connection.Execute("INSERT INTO Reservation (UserID, BoatID, StartTime, Length, Status) VALUES (@UserID, @BoatID, @StartTime, @Length, @Status); SELECT SCOPE_IDENTITY()", res);
        }
        public void Update(ReservationEntity res)
        {
            if (res.ReservationId == 0) return;
            _connection.Execute("UPDATE FROM Reservation WHERE ReservationID = @ReservationID", res);
        }

        public void Delete(ReservationEntity res)
        {
            if (res.ReservationId == 0) return;

            _connection.Execute("DELETE FROM Reservation WHERE ReservationID = @ReservationID", res);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public List<ReservationEntity> Get()
        {
            return _connection.Query<ReservationEntity>("SELECT * FROM Reservation").ToList();
        }

        public List<ReservationEntity> GetByBoatIDAndDay(BoatEntity boat, DateTime day)
            {

            return _connection.Query<ReservationEntity>("SELECT ReservationID, UserID, BoatID, StartTime, Length, Status  FROM Reservation WHERE BoatID = @boatID AND StartTime Like @day AND Status = 3  ORDER BY StartTime", new { day = $"%"+ day.Year + "-" + day.Month+"-"+ day.Day +"%", boat.BoatID }).ToList();
            }

        public List<BoatEntity> GetAvailableBoatsByType(int boatTypeId)
            {
            return _connection.Query<BoatEntity>("SELECT * FROM Boat WHERE BoatTypeId = @boatTypeId AND Status = 0", new { boatTypeId }).ToList();
            }

        public List<BoatTypeEntity> GetAllTypesWithBoats()
            {
            return _connection.Query<BoatTypeEntity>("SELECT * FROM Boattype  WHERE BoattypeID IN (SELECT max(BoattypeID) FROM Boat WHERE Status = 0 GROUP BY BoattypeID)").ToList();
            }


        }
}
