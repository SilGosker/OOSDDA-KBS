
using Kbs.Business.Mock;
using Kbs.Business.User;

namespace Kbs.Business.Session;

public class SessionManagerTests
{
    [Fact]
    public void Constructor_WithNull_ThrowsArgumentNullException()
    {
        // Arrange
        IUserRepository userRepository = null;

        // Act
        var act = () => new SessionManager(userRepository);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Current_AfterConstructing_IsNull()
    {
        // Arrange
        var userRepository = new MockUserRepository();
        var sessionManager = new SessionManager(userRepository);

        // Act
        var session = sessionManager.Current;

        // Assert
        Assert.Null(session);
    }

    [Fact]
    public void TryCreate_WithUnknownUser_ReturnsFalse()
    {
        // Arrange
        var userRepository = new MockUserRepository();
        userRepository.Users.Add(new UserEntity()
        {
            Email = "test@gmail.com",
            Password = "123456"
        });
        var sessionManager = new SessionManager(userRepository);
        var unknownUser = new UserEntity()
        {
            Email = "test2@example.com",
            Password = "7890"
        };

        // Act
        var success = sessionManager.TryCreate(unknownUser, out Session session);

        // Assert
        Assert.False(success);
        Assert.Null(session);
    }

    [Fact]
    public void TryCreate_WithKnownUser_ReturnsTrue()
    {
        // Arrange
        string email = "test@gmail.com";
        string pass = "123456";
        var dbUser = new UserEntity()
        {
            Email = email,
            Password = pass
        };

        var userRepository = new MockUserRepository();
        userRepository.Users.Add(dbUser);
        var sessionManager = new SessionManager(userRepository);
        var knownUser = new UserEntity()
        {
            Email = email,
            Password = pass
        };

        // Act
        var success = sessionManager.TryCreate(knownUser, out Session session);

        // Assert
        Assert.True(success);
        Assert.NotNull(session);
        Assert.Equal(dbUser, session.User);
    }
}