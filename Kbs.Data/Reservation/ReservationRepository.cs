using Dapper;
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
            res.ReservationID = _connection.Execute("INSERT INTO Reservation (ReservationID, UserID, BoatID, StartTime, Length, Status) VALUES (@Email, @Reservation, @UserID @BoatID, @StartTime, @Length, @Status); SELECT SCOPE_IDENTITY()", res);
        }
        public void Update(ReservationEntity res)
        {
            if (res.ReservationID == 0) return;
            _connection.Execute("UPDATE FROM Reservation WHERE ReservationID = @ReservationID", res);
        }


        public void Delete(ReservationEntity res)
        {
            if (res.ReservationID == 0) return;

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

        public List<ReservationEntity> GetByUserId(int userId)
        {
            return _connection.Query<ReservationEntity>("SELECT * FROM Reservation WHERE UserID = @userId", new { userId }).ToList();
        }

        public int GetReservationID()
        {
            var rp = _connection.Query<int>("SELECT FIRST ReservationID From Reservation");
            return rp.FirstOrDefault();
        }

        public ReservationEntity GetById(int id)
        {
            return _connection.Query<ReservationEntity>("SELECT * FROM Reservation WHERE ReservationID = @id", new { id }).FirstOrDefault();

        }
        public List<ReservationEntity> SortByStatus(List<ReservationEntity> reservations)
        {
            return reservations.OrderByDescending(r => r.Status).ToList();
        }
        
    }
}
