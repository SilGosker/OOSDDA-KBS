
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
        var act = () => new SessionManager(userRepository, TimeSpan.MaxValue);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Current_AfterConstructing_IsNull()
    {
        // Arrange
        var userRepository = new MockUserRepository();
        var sessionManager = new SessionManager(userRepository, TimeSpan.MaxValue);

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
        var sessionManager = new SessionManager(userRepository, TimeSpan.MaxValue);
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
        var sessionManager = new SessionManager(userRepository, TimeSpan.MaxValue);
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

    [Fact]
    public void TryCreate_InvokesSessionTimeExpiredEvent_AfterSessionTime()
    {
        // Arrange
        var sessionTime = TimeSpan.FromMilliseconds(100);
        var user = new UserEntity()
        {
            Email = "test@example.com",
            Password = "123456"
        };
        var userRepository = new MockUserRepository();
        userRepository.Users.Add(user);
        var sessionManager = new SessionManager(userRepository, sessionTime);
        bool eventInvoked = false;
        Session expectedSession = null;

        sessionManager.SessionTimeExpired += (_, args) =>
        {
            eventInvoked = true;
            expectedSession = args.Session;
        };

        // Act
        var success = sessionManager.TryCreate(user, out Session session);
        Thread.Sleep(sessionTime.Milliseconds + 50);

        // Assert
        Assert.True(success);
        Assert.True(eventInvoked);
        Assert.Equal(session, expectedSession);
    }

    [Fact]
    public void TryCreate_DoesNotInvokeSessionTimeExpiredEvent_IfSessionIsDisposed()
    {
        // Arrange
        var sessionTime = TimeSpan.FromMilliseconds(100);
        var user = new UserEntity()
        {
            Email = "test@example.com",
            Password = "123456"
        };
        var userRepository = new MockUserRepository();
        userRepository.Users.Add(user);
        var sessionManager = new SessionManager(userRepository, sessionTime);
        bool eventInvoked = false;

        sessionManager.SessionTimeExpired += (_, _) =>
        {
            eventInvoked = true;
        };

        // Act
        var success = sessionManager.TryCreate(user, out Session _);
        sessionManager.Logout();
        Thread.Sleep(sessionTime.Milliseconds + 50);

        // Assert
        Assert.True(success);
        Assert.False(eventInvoked);
    }

    [Fact]
    public void Logout_WithSession_SetsCurrentToNull()
    {
        // Arrange
        var userRepository = new MockUserRepository();
        userRepository.Users.Add(new UserEntity()
        {
            Email = "test@example.com",
            Password = "123456"
        });
        var sessionManager = new SessionManager(userRepository, TimeSpan.MaxValue);
        var user = new UserEntity();
        sessionManager.TryCreate(user, out _);
        
        // Act
        sessionManager.Logout();

        // Assert
        Assert.Null(sessionManager.Current);
    }


    [Theory]
    [InlineData("Test@tester.com", null)]
    [InlineData(null, "12345678")]
    public void UpdateSessionUser_WithValues_GivesTrue(string emailInput, string passwordInput)
    {
        // Arrange
        var userRepository = new MockUserRepository();
        userRepository.Users.Add(new UserEntity()
        {
            Email = "test@example.com",
            Password = "123456"
        });
        var sessionManager = new SessionManager(userRepository, TimeSpan.MaxValue);
        var user = new UserEntity()
        {
            Email = "test@example.com",
            Password = "123456"
        };
        sessionManager.TryCreate(user, out _);

        // Act
        bool result = sessionManager.UpdateSessionUser(emailInput, passwordInput);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void UpdateSessionUser_WithBothNullValues_GivesTrue()
    {
        // Arrange
        var userRepository = new MockUserRepository();
        userRepository.Users.Add(new UserEntity()
        {
            Email = "test@example.com",
            Password = "123456"
        });
        var sessionManager = new SessionManager(userRepository, TimeSpan.MaxValue);
        var user = new UserEntity()
        {
            Email = "test@example.com",
            Password = "123456"
        };
        sessionManager.TryCreate(user, out _);

        // Act
        bool result = sessionManager.UpdateSessionUser(null, null);

        // Assert
        Assert.False(result);
    }
}