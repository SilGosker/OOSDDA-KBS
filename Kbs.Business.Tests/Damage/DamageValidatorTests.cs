namespace Kbs.Business.Damage;

public class DamageValidatorTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("\t\t")]
    [InlineData("  ")]
    public void ValidateForUpload_WithEmptyDescription_ShouldReturnError(string description)
    {
        // Arrange
        var validator = new DamageValidator();
        var entity = new DamageEntity()
        {
            Description = description,
            Image = new byte[] { 0xFF, 0xD8, 0xFF } // valid image header
        };

        // Act
        var result = validator.ValidateForUpload(entity);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.ContainsKey(nameof(entity.Description)));
        Assert.Equal("Omschrijving is verplicht", result[nameof(entity.Description)]);
    }

    [Fact]
    public void ValidateForUpload_WithNullImage_ShouldReturnError()
    {
        // Arrange
        var validator = new DamageValidator();
        var entity = new DamageEntity()
        {
            Description = "Description",
            Image = null
        };
        // Act
        var result = validator.ValidateForUpload(entity);
        // Assert
        Assert.NotNull(result);
        Assert.True(result.ContainsKey(nameof(entity.Image)));
        Assert.Equal("Afbeelding is verplicht", result[nameof(entity.Image)]);
    }

    [Fact]
    public void ValidateForUpload_WithInvalidImageHeader_ShouldReturnError()
    {
        // Arrange
        var validator = new DamageValidator();
        var entity = new DamageEntity()
        {
            Description = "Description",
            Image = new byte[] { 0x00, 0x00, 0x00 } // invalid image header
        };
        // Act
        var result = validator.ValidateForUpload(entity);
        // Assert
        Assert.NotNull(result);
        Assert.True(result.ContainsKey(nameof(entity.Image)));
        Assert.Equal("Afbeelding is geen PNG of JPG", result[nameof(entity.Image)]);
    }

    [Fact]
    public void ValidateForUpload_WithValidImageHeader_ShouldReturnNoError()
    {
        // Arrange
        var validator = new DamageValidator();
        var entity = new DamageEntity()
        {
            Description = "Description",
            Image = new byte[] { 0xFF, 0xD8, 0xFF } // valid image header
        };
        // Act
        var result = validator.ValidateForUpload(entity);
        // Assert
        Assert.NotNull(result);
        Assert.False(result.ContainsKey(nameof(entity.Image)));
    }

    [Fact]
    public void ValidateForUpload_WithValidImageHeaderAndDescription_ShouldReturnNoError()
    {
        // Arrange
        var validator = new DamageValidator();
        var entity = new DamageEntity()
        {
            Description = "Description",
            Image = new byte[] { 0xFF, 0xD8, 0xFF } // valid image header
        };
        // Act
        var result = validator.ValidateForUpload(entity);
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}