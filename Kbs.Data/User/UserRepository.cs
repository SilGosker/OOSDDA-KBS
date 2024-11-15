using Kbs.Business.User;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Kbs.Data.User;

public class UserRepository : IUserRepository, IDisposable
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);

    public void Create(UserEntity user)
    {
        _connection.Execute("INSERT INTO Users (Email, Password, Name, Role) VALUES (@Email, @Password, @Name, @Role)", user);
        int id = _connection.Query<int>("SELECT LAST_INSERT_ID()").First();
        user.UserId = id;
    }

    public void Update(UserEntity user)
    {
        if (user.UserId == 0) return;

        _connection.Execute("UPDATE Users SET Email = @Email, Password = @Password, Name = @Name, Role = @Role WHERE UserId = @UserId", user);
    }

    public void Delete(UserEntity user)
    {
        if (user.UserId == 0) return;

        _connection.Execute("DELETE FROM Users WHERE UserId = @UserId", user);
    }

    public List<UserEntity> Get()
    {
        return _connection.Query<UserEntity>("SELECT * FROM Users").ToList();
    }

    public UserEntity GetByCredentials(string email, string password)
    {
        var user = _connection.Query<UserEntity>("SELECT * FROM Users WHERE Email = @Email", new { Email = email }).FirstOrDefault();

        if (user == null)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }

        return user;
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}