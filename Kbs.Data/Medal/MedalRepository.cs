using Dapper;
using Kbs.Business.Medal;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.Medal
{
    public class MedalRepository : IMedalRepository
    {
        private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);
        public void Create(MedalEntity medal)
        {
            // medal as material to map properties correctly
            medal.MedalId = _connection.QueryFirst<int>(
            "INSERT INTO Medal (BoatId, UserId, GameId, Medal) VALUES (@BoatId, @UserId, @GameId, @Material); SELECT SCOPE_IDENTITY()",
            medal);
        }

        public List<MedalEntity> GetAllByUserId(int userId)
        {
            return _connection.Query<MedalEntity>("SELECT *, m.Medal as Material FROM Medal m WHERE UserID = @userId", new { userId }).ToList();
        }

        public void RemoveById(int medalId)
        {
            _connection.Execute("DELETE FROM Medal WHERE MedalID = @medalId", new { medalId });
        }

        public List<MedalEntity> GetAllByGameId(int gameId)
        {
            return _connection.Query<MedalEntity>("SELECT *, m.Medal as Material FROM Medal m WHERE GameID = @gameId", new { gameId }).ToList();
        }
    }
}
