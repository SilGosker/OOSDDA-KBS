using Kbs.Business.User;

namespace Kbs.Business.Session;

public class SessionTests
{
    [Fact]
    public void Constructor_WithNull_ThrowsArgumentNullException()
    {
        // Arrange
        UserEntity user = null;

        // Act
        var act = () => new Session(user);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Constructor_WithUserEntity_SetsUser()
    {
        // Arrange
        var user = new UserEntity();

        // Act
        var session = new Session(user);

        // Assert
        Assert.Equal(user, session.User);
    }
}