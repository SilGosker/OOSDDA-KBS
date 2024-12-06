namespace Kbs.Business.Game;

public class GameEntityTests
{
    [Theory]
    [InlineData(1, "Game", 1)]
    [InlineData(int.MaxValue, "", int.MaxValue)]
    [InlineData(int.MinValue, null, int.MinValue)]
    public void Properties_ShouldSetValues(int gameId, string name, int courseId)
    {
        // Arrange
        var game = new GameEntity();
        // Act
        game.GameId = gameId;
        game.Name = name;
        game.Date = DateTime.MaxValue;
        game.CourseId = courseId;
        // Assert
        Assert.Equal(gameId, game.GameId);
        Assert.Equal(name, game.Name);
        Assert.Equal(DateTime.MaxValue, game.Date);
        Assert.Equal(courseId, game.CourseId);
    }


}
