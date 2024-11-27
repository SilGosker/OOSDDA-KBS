namespace Kbs.Business.Helpers;

public class ThrowHelperTests
{
    [Fact]
    public void ThrowIfNull_WhenObjectIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        object obj = null;

        // Act
        Action act = () => ThrowHelper.ThrowIfNull(obj);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void ThrowIfNullWhenObjectIsNotNull_DoesNotThrowException()
    {
        // Arrange
        object obj = new object();

        // Act & Assert
        ThrowHelper.ThrowIfNull(obj);
    }
}