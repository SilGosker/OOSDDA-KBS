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
    public void ValidateForCreate_WhenNameIsLargerThan255Characters_ShouldReturnError()
    {
        // Arrange
        var boatType = new BoatTypeEntity()
        {
            Name = new string('a', 256),
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
        Assert.Equal("Naam mag niet langer zijn dan 255 karakters", errorMessage);
    }
    
    [Fact]
    public void ValidateForUpdate_WhenNameIsLargerThan255Characters_ShouldReturnError()
    {
        // Arrange
        var boatType = new BoatTypeEntity()
        {
            Name = new string('a', 256),
            RequiredExperience = BoatTypeRequiredExperience.Beginner,
            Seats = BoatTypeSeats.Two,
            Speed = 100
        };
        var validator = new BoatTypeValidator();

        // Act
        var validationResult = validator.ValidatorForUpdate(boatType);

        // Assert
        Assert.NotNull(validationResult);
        Assert.Single(validationResult);
        Assert.True(validationResult.TryGetValue(nameof(boatType.Name), out string errorMessage));
        Assert.Equal("Naam mag niet langer zijn dan 255 karakters", errorMessage);
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
    
    [Fact]
    public void ValidatorForUpdate_ShouldReturnError_WhenSpeedIsZeroOrNegative()
    {
        // Arrange
        var validator = new BoatTypeValidator();
        var boatType = new BoatTypeEntity
        {
            Name = "Speedboat",
            Speed = 0,
            RequiredExperience = BoatTypeRequiredExperience.Beginner,
            Seats = BoatTypeSeats.One
        };

        // Act
        var result = validator.ValidatorForUpdate(boatType);

        // Assert
        Assert.True(result.ContainsKey(nameof(boatType.Speed)));
        Assert.Equal("Snelheid moet groter zijn dan 0", result[nameof(boatType.Speed)]);
    }

    [Fact]
    public void ValidatorForUpdate_ShouldReturnEmptyDictionary_WhenValuesAreValid()
    {
        // Arrange
        var validator = new BoatTypeValidator();
        var boatType = new BoatTypeEntity
        {
            Name = "Speedboat",
            Speed = 10,
            RequiredExperience = BoatTypeRequiredExperience.Beginner,
            Seats = BoatTypeSeats.One
        };

        // Act
        var result = validator.ValidatorForUpdate(boatType);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ValidateForCreate_ShouldReturnError_WhenNameIsNullOrWhitespace()
    {
        // Arrange
        var validator = new BoatTypeValidator();
        var boatType = new BoatTypeEntity { Name = "  " }; // whitespace

        // Act
        var result = validator.ValidateForCreate(boatType);

        // Assert
        Assert.Contains("Name", result);
        Assert.Equal("Naam is verplicht", result["Name"]);
    }

    [Fact]
    public void ValidateForCreate_ShouldReturnError_WhenRequiredExperienceIsDefault()
    {
        // Arrange
        var validator = new BoatTypeValidator();
        var boatType = new BoatTypeEntity { RequiredExperience = default };

        // Act
        var result = validator.ValidateForCreate(boatType);

        // Assert
        Assert.Contains("RequiredExperience", result);
        Assert.Equal("Benodigde ervaring is verplicht", result["RequiredExperience"]);
    }

    [Fact]
    public void ValidateForCreate_ShouldReturnError_WhenSeatsIsInvalid()
    {
        // Arrange
        var validator = new BoatTypeValidator();
        var boatType = new BoatTypeEntity { Seats = (BoatTypeSeats)999 }; // invalid enum value

        // Act
        var result = validator.ValidateForCreate(boatType);

        // Assert
        Assert.Contains("Seats", result);
        Assert.Equal("Stoelen zijn verplicht", result["Seats"]);
    }

    [Fact]
    public void ValidateForCreate_ShouldReturnError_WhenSpeedIsZeroOrNegative()
    {
        // Arrange
        var validator = new BoatTypeValidator();

        var boatTypeZeroSpeed = new BoatTypeEntity { Speed = 0 };
        var boatTypeNegativeSpeed = new BoatTypeEntity { Speed = -1 };

        // Act
        var resultZeroSpeed = validator.ValidateForCreate(boatTypeZeroSpeed);
        var resultNegativeSpeed = validator.ValidateForCreate(boatTypeNegativeSpeed);

        // Assert
        Assert.Contains("Speed", resultZeroSpeed);
        Assert.Equal("Snelheid is verplicht en moet groter zijn dan 0", resultZeroSpeed["Speed"]);

        Assert.Contains("Speed", resultNegativeSpeed);
        Assert.Equal("Snelheid is verplicht en moet groter zijn dan 0", resultNegativeSpeed["Speed"]);
    }

    [Fact]
    public void ValidateForCreate_ShouldReturnEmpty_WhenValidBoatType()
    {
        // Arrange
        var validator = new BoatTypeValidator();
        var boatType = new BoatTypeEntity
        {
            Name = "Speedboat",
            RequiredExperience = BoatTypeRequiredExperience.Intermediate,
            Seats = BoatTypeSeats.Four,
            Speed = 10
        };

        // Act
        var result = validator.ValidateForCreate(boatType);

        // Assert
        Assert.Empty(result); // No validation errors
    }
}