using Dapper;
using Kbs.Business.Game;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.Game;

public class GameRepository : IGameRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);
    public void Create(GameEntity game)
    {
        game.GameId = _connection.QuerySingle<int>("INSERT INTO Game (Name, Date, CourseID) VALUES (@Name, @Date, @CourseId); SELECT SCOPE_IDENTITY();", game);
    }
}