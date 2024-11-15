using Kbs.Business.User;

namespace Kbs.Business.Session;

public class SessionTimeExpiredEventArgsTests
{
    [Fact]
    public void Constructor_WithNull_ShouldThrowArgumentNullException()
    {
        // Assert
        Session session = null;

        // Act
        var act = () => new SessionTimeExpiredEventArgs(session);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Constructor_WithEvent_ShouldSetSessionProperty()
    {
        // Arrange
        var session = new Session(new UserEntity());

        // Act
        var eventArgs = new SessionTimeExpiredEventArgs(session);

        // Assert
        Assert.Equal(session, eventArgs.Session);
    }
}