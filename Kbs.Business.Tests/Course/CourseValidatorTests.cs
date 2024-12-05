namespace Kbs.Business.Course;

public class CourseValidatorTests
{
    [Fact]
    public void ValidateForCreate_WhenNameIsEmpty_ShouldReturnError()
    {
        // Arrange
        var course = new CourseEntity()
        {
            Name = string.Empty,
            Difficulty = CourseDifficulty.Easy,
            Image = new byte[] { 0xFF, 0xD8, 0xFF }
        };
        var validator = new CourseValidator();

        // Act
        var validationResult = validator.ValidateForCreate(course);

        // Assert
        Assert.NotNull(validationResult);
        Assert.Single(validationResult);
        Assert.True(validationResult.TryGetValue(nameof(course.Name), out string errorMessage));
        Assert.Equal("Naam is verplicht", errorMessage);
    }

    [Fact]
    public void ValidateForCreate_WhenDifficultyIsEmpty_ShouldReturnError()
    {
        // Arrange
        var course = new CourseEntity()
        {
            Name = "Course",
            Difficulty = default,
            Image = new byte[] { 0xFF, 0xD8, 0xFF }
        };
        var validator = new CourseValidator();

        // Act
        var validationResult = validator.ValidateForCreate(course);

        // Assert
        Assert.NotNull(validationResult);
        Assert.Single(validationResult);
        Assert.True(validationResult.TryGetValue(nameof(course.Difficulty), out string errorMessage));
        Assert.Equal("Moeilijkheid is verplicht", errorMessage);
    }
    [Fact]
    public void ValidateForUpload_WithNullImage_ShouldReturnError()
    {
        // Arrange
        var course = new CourseEntity()
        {
            Name = "Course",
            Difficulty = CourseDifficulty.Easy,
            Image = null
        };
        var validator = new CourseValidator();
        // Act
        var result = validator.ValidateForCreate(course);
        // Assert
        Assert.NotNull(result);
        Assert.True(result.ContainsKey(nameof(course.Image)));
        Assert.Equal("Afbeelding is verplicht", result[nameof(course.Image)]);
    }

    [Fact]
    public void ValidateForUpload_WithInvalidImageHeader_ShouldReturnError()
    {
        // Arrange
        var course = new CourseEntity()
        {
            Name = "Course",
            Difficulty = CourseDifficulty.Easy,
            Image = new byte[] { 0x00, 0x00, 0x00 } // invalid image header
        };
        var validator = new CourseValidator();
        // Act
        var result = validator.ValidateForCreate(course);
        // Assert
        Assert.NotNull(result);
        Assert.True(result.ContainsKey(nameof(course.Image)));
        Assert.Equal("Afbeelding is geen PNG of JPG", result[nameof(course.Image)]);
    }

    [Fact]
    public void ValidateForUpload_WithValidImageHeader_ShouldReturnNoError()
    {
        // Arrange
        var course = new CourseEntity()
        {
            Name = "Course",
            Difficulty = CourseDifficulty.Easy,
            Image = new byte[] { 0xFF, 0xD8, 0xFF }
        };
        var validator = new CourseValidator();
        // Act
        var result = validator.ValidateForCreate(course);
        // Assert
        Assert.NotNull(result);
        Assert.False(result.ContainsKey(nameof(course.Image)));
    }

    [Fact]
    public void ValidateForUpload_WithValidImageHeaderAndDescription_ShouldReturnNoError()
    {
        // Arrange
        var course = new CourseEntity()
        {
            Name = "Course",
            Difficulty = CourseDifficulty.Easy,
            Image = new byte[] { 0xFF, 0xD8, 0xFF }
        };
        var validator = new CourseValidator();
        // Act
        var result = validator.ValidateForCreate(course);
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
