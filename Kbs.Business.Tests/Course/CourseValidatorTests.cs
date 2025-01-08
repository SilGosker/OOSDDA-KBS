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
    public void ValidateForCreate_WhenNameIsLargerThan255Characters_ShouldReturnError()
    {
        // Arrange
        var course = new CourseEntity()
        {
            Name = new string('a', 256),
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
        Assert.Equal("Naam mag niet langer zijn dan 255 karakters", errorMessage);
    }
    
    [Fact]
    public void ValidateForUpdate_WhenNameIsLargerThan255Characters_ShouldReturnError()
    {
        // Arrange
        var course = new CourseEntity()
        {
            Name = new string('a', 256),
            Difficulty = CourseDifficulty.Easy,
            Image = new byte[] { 0xFF, 0xD8, 0xFF }
        };
        var validator = new CourseValidator();

        // Act
        var validationResult = validator.ValidateForUpdate(course);

        // Assert
        Assert.NotNull(validationResult);
        Assert.Single(validationResult);
        Assert.True(validationResult.TryGetValue(nameof(course.Name), out string errorMessage));
        Assert.Equal("Naam mag niet langer zijn dan 255 karakters", errorMessage);
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
        var result = validator.ValidateForCreate(course);// Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

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
