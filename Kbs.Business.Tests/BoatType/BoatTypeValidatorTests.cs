namespace Kbs.Business.BoatType;

public class BoatTypeValidatorTests
{
    [Fact]
    public void ValidateForCreate_WhenNameIsEmpty_ShouldReturnError()
    {
        // Arrange
        var boatType = new BoatTypeEntity()
        {
            Name = string.Empty,
            RequiredExperience = BoatTypeRequiredExperience.Beginner,
            Seats = BoatTypeSeats.Two,
            Speed = 100
        };
        var validator = new BoatTypeValidator();

        // Act
        var validationResult = validator.ValidateForCreate(boatType);

        // Assert
        Assert.NotNull(validationResult);
        Assert.Single(validationResult);
        Assert.True(validationResult.TryGetValue(nameof(boatType.Name), out string errorMessage));
        Assert.Equal("Naam is verplicht", errorMessage);
    }

    [Fact]
    public void ValidateForCreate_WhenRequiredExperienceIsDefault_ShouldReturnError()
    {
        // Arrange
        var boatType = new BoatTypeEntity()
        {
            Seats = BoatTypeSeats.Two,
            Name = "test",
            Speed = 100,
            RequiredExperience = default
        };
        var validator = new BoatTypeValidator();

        // Act
        var validationResult = validator.ValidateForCreate(boatType);

        // Assert
        Assert.NotNull(validationResult);
        Assert.Single(validationResult);
        Assert.True(validationResult.TryGetValue(nameof(boatType.RequiredExperience), out string errorMessage));
        Assert.Equal("Benodigde ervaring is verplicht", errorMessage);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void ValidateForCreate_WhenSpeedIsLessThanOne_ShouldReturnError(int speed)
    {
        // Arrange
        var boatType = new BoatTypeEntity()
        {
            Seats = BoatTypeSeats.Two,
            Name = "test",
            Speed = speed,
            RequiredExperience = BoatTypeRequiredExperience.Beginner
        };
        var validator = new BoatTypeValidator();

        // Act
        var validationResult = validator.ValidateForCreate(boatType);

        // Assert
        Assert.NotNull(validationResult);
        Assert.Single(validationResult);
        Assert.True(validationResult.TryGetValue(nameof(boatType.Speed), out string errorMessage));
        Assert.Equal("Snelheid is verplicht en moet groter zijn dan 0", errorMessage);
    }

    [Fact]
    public void ValidateForCreate_WhenInvalidEntity_ShouldReturnMultipleErrors()
    {
        // Arrange
        var boatType = new BoatTypeEntity();
        var validator = new BoatTypeValidator();

        // Act
        var validationResult = validator.ValidateForCreate(boatType);

        // Assert
        Assert.NotNull(validationResult);
        Assert.Equal(4, validationResult.Count);
    }

    [Fact]
    public void ValidateForCreate_WhenValidEntity_ShouldReturnNoErrors()
    {
        // Arrange
        var boatType = new BoatTypeEntity()
        {
            Seats = BoatTypeSeats.Two,
            Name = "test",
            Speed = 100,
            RequiredExperience = BoatTypeRequiredExperience.Beginner
        };
        var validator = new BoatTypeValidator();

        // Act
        var validationResult = validator.ValidateForCreate(boatType);

        // Assert
        Assert.NotNull(validationResult);
        Assert.Empty(validationResult);
    }
}