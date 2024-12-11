namespace Kbs.Business.Game;

public class GameValidatorTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("\t\t")]
    public void ValidateForCreate_WithEmptyName_ReturnsError(string name)
    {
        // Arrange
        var game = new GameEntity
        {
            Name = name,
            Date = DateTime.Now.AddDays(1),
            CourseId = 1
        };
        var validator = new GameValidator();

        // Act
        var result = validator.ValidateForCreate(game);

        // Assert
        Assert.Single(result);
        Assert.True(result.TryGetValue(nameof(game.Name), out string nameError));
        Assert.Equal("Naam is verplicht", nameError);
    }

    [Fact]
    public void ValidateForCreate_WithPastDate_ReturnsError()
    {
        // Arrange
        var game = new GameEntity
        {
            Name = "Test",
            Date = DateTime.Now.AddDays(-1),
            CourseId = 1
        };
        var validator = new GameValidator();

        // Act
        var result = validator.ValidateForCreate(game);

        // Assert
        Assert.Single(result);
        Assert.True(result.TryGetValue(nameof(game.Date), out string dateError));
        Assert.Equal("Datum moet in de toekomst liggen", dateError);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(int.MinValue)]
    public void ValidateForCreate_WithNullCourseId_ReturnsError(int courseId)
    {
        // Arrange
        var game = new GameEntity
        {
            Name = "Test",
            Date = DateTime.Now.AddDays(1),
            CourseId = courseId
        };

        var validator = new GameValidator();

        // Act
        var result = validator.ValidateForCreate(game);

        // Assert
        Assert.Single(result);
        Assert.True(result.TryGetValue(nameof(game.CourseId), out string courseIdError));
        Assert.Equal("Parcours is verplicht", courseIdError);
    }

    [Fact]
    public void ValidateForCreate_WithValidGame_ReturnsEmptyDictionary()
    {
        // Arrange
        var game = new GameEntity
        {
            Name = "Test",
            Date = DateTime.Now.AddDays(1),
            CourseId = 1
        };
        var validator = new GameValidator();

        // Act
        var result = validator.ValidateForCreate(game);

        // Assert
        Assert.Empty(result);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ValidateForUpdate_WithEmptyName_ReturnsError(string name)
    {
        // Arrange
        var game = new GameEntity
        {
            Name = name,
            Date = DateTime.Now.AddDays(1),
            CourseId = 1
        };
        var validator = new GameValidator();

        // Act
        var result = validator.ValidateForUpdate(game);

        // Assert
        Assert.Single(result);
        Assert.True(result.TryGetValue(nameof(game.Name), out string nameError));
        Assert.Equal("Naam is verplicht", nameError);
    }

    [Fact]
    public void ValidateForUpdate_WithPastDate_ReturnsError()
    {
        // Arrange
        var game = new GameEntity
        {
            Name = "Test",
            Date = DateTime.Now.AddDays(-1),
            CourseId = 1
        };
        var validator = new GameValidator();

        // Act
        var result = validator.ValidateForUpdate(game);

        // Assert
        Assert.Single(result);
        Assert.True(result.TryGetValue(nameof(game.Date), out string dateError));
        Assert.Equal("Datum moet in de toekomst liggen", dateError);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(int.MinValue)]
    public void ValidateForUpdate_WithInvalidCourseId_ReturnsError(int courseId)
    {
        // Arrange
        var game = new GameEntity
        {
            Name = "Test",
            Date = DateTime.Now.AddDays(1),
            CourseId = courseId
        };
        var validator = new GameValidator();

        // Act
        var result = validator.ValidateForUpdate(game);

        // Assert
        Assert.Single(result);
        Assert.True(result.TryGetValue(nameof(game.CourseId), out string courseIdError));
        Assert.Equal("Parcours is verplicht", courseIdError);
    }

    [Fact]
    public void ValidateForUpdate_WithValidGame_ReturnsEmptyDictionary()
    {
        // Arrange
        var game = new GameEntity
        {
            Name = "Valid Game",
            Date = DateTime.Now.AddDays(1),
            CourseId = 1
        };
        var validator = new GameValidator();

        // Act
        var result = validator.ValidateForUpdate(game);

        // Assert
        Assert.Empty(result);
    }
}