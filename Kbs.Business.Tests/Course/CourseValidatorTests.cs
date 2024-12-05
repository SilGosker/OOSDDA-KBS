using Kbs.Business.Damage;

namespace Kbs.Business.Course;

public class CourseValidatorTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("\t\t")]
    [InlineData("  ")]
    public void ValidateForUpdate_WithEmptyDescription_ShouldReturnError(string name)
    {
        // Arrange
        var validator = new CourseValidator();
        var entity = new CourseEntity()
        {
            Name = name,
            Image = new byte[] { 0xFF, 0xD8, 0xFF } // valid image header
        };

        // Act
        var result = validator.ValidateForUpdate(entity);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.ContainsKey(nameof(entity.Name)));
        Assert.Equal("Naam is verplicht", result[nameof(entity.Name)]);
    }

    [Fact]
    public void ValidateForUpdate_WithNullImage_ShouldReturnError()
    {
        // Arrange
        var validator = new CourseValidator();
        var entity = new CourseEntity()
        {
            Name = "Description",
            Image = null
        };
        // Act
        var result = validator.ValidateForUpdate(entity);
        // Assert
        Assert.NotNull(result);
        Assert.True(result.ContainsKey(nameof(entity.Image)));
        Assert.Equal("Afbeelding is verplicht", result[nameof(entity.Image)]);
    }

    [Fact]
    public void ValidateForUpdate_WithInvalidImageHeader_ShouldReturnError()
    {
        // Arrange
        var validator = new CourseValidator();
        var entity = new CourseEntity()
        {
            Name = "Description",
            Image = new byte[] { 0x00, 0x00, 0x00 } // invalid image header
        };
        // Act
        var result = validator.ValidateForUpdate(entity);
        // Assert
        Assert.NotNull(result);
        Assert.True(result.ContainsKey(nameof(entity.Image)));
        Assert.Equal("Afbeelding is geen PNG of JPG", result[nameof(entity.Image)]);
    }

    [Fact]
    public void ValidateForUpdate_WithValidImageHeader_ShouldReturnNoError()
    {
        // Arrange
        var validator = new CourseValidator();
        var entity = new CourseEntity()
        {
            Name = "Description",
            Image = new byte[] { 0xFF, 0xD8, 0xFF } // valid image header
        };
        // Act
        var result = validator.ValidateForUpdate(entity);
        // Assert
        Assert.NotNull(result);
        Assert.False(result.ContainsKey(nameof(entity.Image)));
    }

    [Fact]
    public void ValidateForUpdate_WithDefaultDifficulty_ShouldReturnError()
    {
        // Arrange
        var validator = new CourseValidator();
        var entity = new CourseEntity()
        {
            Name = "Name",
            Image = new byte[] { 0xFF, 0xD8, 0xFF }, // valid image header
            Difficulty = default
        };
        // Act
        var result = validator.ValidateForUpdate(entity);
        // Assert
        Assert.NotNull(result);
        Assert.True(result.ContainsKey(nameof(entity.Difficulty)));
        Assert.Equal("Moeilijkheidsgraad is verplicht", result[nameof(entity.Difficulty)]);
    }

    [Fact]
    public void ValidateForUpdate_WithValidImageHeaderAndDescription_ShouldReturnNoError()
    {
        // Arrange
        var validator = new CourseValidator();
        var entity = new CourseEntity()
        {
            Name = "Name",
            Difficulty = CourseDifficulty.VeryHard,
            Image = new byte[] { 0xFF, 0xD8, 0xFF } // valid image header
            
        };
        // Act
        var result = validator.ValidateForUpdate(entity);
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}