using Dapper;
using Kbs.Business.Boat;
using Kbs.Business.Game;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.Game;

public class GameRepository : IGameRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);

    public void Create(GameEntity game)
    {
        game.GameId = _connection.QuerySingle<int>(
            "INSERT INTO Game (Name, Date, CourseID) VALUES (@Name, @Date, @CourseId); SELECT SCOPE_IDENTITY();", game);
    }

    public GameEntity GetById(int gameId)
    {
        return _connection.QueryFirstOrDefault<GameEntity>("SELECT * FROM Game WHERE GameID = @gameId", new { gameId });
    }

    public List<GameEntity> GetMany()
    {
        return _connection.Query<GameEntity>("SELECT * FROM Game").ToList();
    }

    public List<GameEntity> GetManyByCourse(int courseId)
    {
        return _connection.Query<GameEntity>("SELECT * FROM Game WHERE CourseID = @courseId", new { courseId }).ToList();
    }

    public List<GameEntity> GetManyByName(string name)
    {
        return _connection.Query<GameEntity>("SELECT * FROM Game WHERE LOWER(Name) LIKE @name", new { name = $"%{name.ToLower()}%" }).ToList();
    }

    public List<GameEntity> GetManyByNameAndCourse(string name, int courseId)
    {
        return _connection.Query<GameEntity>("SELECT * FROM Game WHERE LOWER(Name) LIKE @name AND CourseID = @courseId", new { name = $"%{name.ToLower()}%", courseId }).ToList();
    }

    public void DeleteById(int gameId)
    {
        _connection.Execute("DELETE FROM Reservation WHERE GameId = @gameId", new { gameId });
        _connection.Execute("DELETE FROM Game WHERE GameId = @gameId", new { gameId });
    }

    public void Update(GameEntity game)
    {
        _connection.Execute("UPDATE Game SET CourseID = @CourseId, Name = @Name, Date = @Date WHERE GameId = @GameId", game);
    }
    
}