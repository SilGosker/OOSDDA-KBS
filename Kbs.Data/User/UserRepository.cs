using Kbs.Business.User;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Kbs.Data.User;

public class UserRepository : IUserRepository, IDisposable
{
    private readonly SqlConnection _connection = new(DatabaseConstants.ConnectionString);

    public void Create(UserEntity user)
    {
        user.UserId = _connection.Execute("INSERT INTO Users (Email, Password, Name, Role) VALUES (@Email, @Password, @Name, @Role); SELECT SCOPE_IDENTITY()", user);
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

    public UserEntity GetByEmail(string email)
    {
        var user = _connection.Query<UserEntity>("SELECT * FROM Users WHERE Email = @Email", new { Email = email })
            .FirstOrDefault();

        return user;
    }
    
    public UserEntity GetById(int id)
    {
        return _connection.Query<UserEntity>("SELECT * FROM Users WHERE UserId = @UserId", new { UserId = id }).FirstOrDefault();
    }
    public void ChangeRole(UserEntity user)
    {
        _connection.Execute("UPDATE Users SET Role = 11 WHERE UserID = @UserId",new { UserId = user.UserId });
        _connection.Execute("UPDATE Reservation SET Status = 11 WHERE UserID = @UserId", new { UserId = user.UserId });
    }


    public void Dispose()
    {
        _connection?.Dispose();
    }
    public IEnumerable<UserEntity> GetUsersByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return _connection.Query<UserEntity>("SELECT * FROM Users");
        }
        return _connection.Query<UserEntity>(
            "SELECT * FROM Users WHERE Name LIKE @Name",
            new { Name = "%" + name + "%" });
    }

    public IEnumerable<UserRole> GetAllRoles()
    {
        return _connection.Query<UserRole>("SELECT DISTINCT Role FROM Users");
    }
    public IEnumerable<UserEntity> GetUsersByNameAndRole(string name, UserRole role)
    {
        var sql = @"SELECT * FROM Users WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%') AND (Role & @Role) != 0";
        return _connection.Query<UserEntity>(sql, new { Name = name, Role = (int)role });
    }

}