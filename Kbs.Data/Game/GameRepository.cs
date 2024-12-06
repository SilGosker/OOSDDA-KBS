using Dapper;
using Kbs.Business.Game;
using Kbs.Business.User;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.Game;

public class GameRepository : IGameRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);
    public void Create(GameEntity game)
    {
        game.GameId = _connection.QuerySingle<int>("INSERT INTO Game (Name, Date, CourseID) VALUES (@Name, @Date, @CourseId); SELECT SCOPE_IDENTITY();", game);
    }
    public List<GameEntity> GetByGameId(int id)
    {
        return _connection.Query<List<GameEntity>>("SELECT * FROM Game WHERE GameId = @GameId", new { GameId = id }).FirstOrDefault();
    }
   
}