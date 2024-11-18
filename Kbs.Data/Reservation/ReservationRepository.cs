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
            res.ReservationId = _connection.Execute("INSERT INTO Reservation (ReservationID, UserID, BoatID, StartTime, Length, Status) VALUES (@Email, @Reservation, @UserID @BoatID, @StartTime, @Length, @Status); SELECT SCOPE_IDENTITY()", res);
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

        /*
        public void GetStatus()
        {
            Console.WriteLine(_connection.Query("SELECT status FROM Reservation"));
        }
        public IEnumerable<dynamic> GetTijdsduur()
        {
            return _connection.Query("SELECT Length FROM Reservation");
        }
        */
        public int GetReservationID()
        {
            var rp = _connection.Query<int>("SELECT FIRST ReservationID From Reservation");
            return rp.FirstOrDefault();

            /*
            var rp1 = _connection.Query<int>("SELECT ReservationID FROM Reservation WHERE UserID = @UserID");
            return rp1.FirstOrDefault();
            */
        }

    }
}
