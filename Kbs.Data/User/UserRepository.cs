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
    
    public UserEntity GetUserIdByBoatTypeId(int boatTypeId)
    {
        const string query = @"
    SELECT u.*
    FROM BoatType bt
    INNER JOIN Boat b ON bt.BoatTypeID = b.BoatTypeID
    INNER JOIN Reservation r ON b.BoatID = r.BoatID
    INNER JOIN Users u ON r.UserID = u.UserID
    WHERE bt.BoatTypeID = @BoatTypeID";

        return _connection.Query<UserEntity>(query, new { BoatTypeId = boatTypeId }).FirstOrDefault();
    }

    public UserEntity GetById(int id)
    {
        return _connection.Query<UserEntity>("SELECT * FROM Users WHERE UserId = @UserId", new { UserId = id }).FirstOrDefault();
    }


    public void Dispose()
    {
        _connection?.Dispose();
    }
}