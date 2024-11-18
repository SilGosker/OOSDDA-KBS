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
            res.ReservationId = _connection.Execute("INSERT INTO Users (Email, Password, Name, Role) VALUES (@Email, @Password, @Name, @Role); SELECT SCOPE_IDENTITY()", res);
        }
        public void Update(ReservationEntity reservation)
        {
            throw new NotImplementedException();
        }

        public void Delete(ReservationEntity res)
        {
            if (res.ReservationId == 0) return;

            _connection.Execute("DELETE FROM Users WHERE UserId = @UserId", user);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public List<ReservationEntity> Get()
        {
            return _connection.Query<ReservationEntity>("SELECT * FROM Reservation").ToList();
        }
        
    }
}
