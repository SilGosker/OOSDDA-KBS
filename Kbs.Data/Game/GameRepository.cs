using Dapper;
using Kbs.Business.Game;
using Microsoft.Data.SqlClient;

namespace Kbs.Data.Game;

public class GameRepository : IGameRepository
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);
    public void Create(GameEntity game)
    {
        _connection.Execute("INSERT INTO Game (Name, Date) VALUES (@Name, @Date)", game);
    }
}