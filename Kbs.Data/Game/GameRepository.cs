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
    public GameEntity GetById(int id)
    {
        return _connection.QueryFirstOrDefault<GameEntity>("SELECT * FROM Game WHERE GameId = @GameId", new { GameId = id });
    }

    public List<GameEntity> Get()
    {
        return _connection.Query<List<GameEntity>>("SELECT * FROM Game").FirstOrDefault();
    }
}