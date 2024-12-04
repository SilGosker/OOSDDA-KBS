using Kbs.Business.Mock;

namespace Kbs.Business.Boat;

public class BoatValidatorTests
{

    [Fact]
    public void ValidateForCreate_ShouldReturnError_WhenBoatIsNull()
    {
        // Arrange
        BoatEntity boat = null;
        var validator = new BoatValidator(new MockBoatTypeRepository());

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => validator.ValidateForCreate(boat));
        Assert.Equal("Value cannot be null. (Parameter 'boat')", exception.Message);
    }

    [Fact]
    public void ValidateForCreate_ShouldReturnError_WhenBoatNameIsNullOrWhitespace()
    {
        // Arrange
        var boat = new BoatEntity
        {
            Name = null,
            BoatTypeId = 1
        };
        var validator = new BoatValidator(new MockBoatTypeRepository());

        // Act
        var result = validator.ValidateForCreate(boat);

        // Assert
        Assert.Contains(nameof(BoatEntity.Name), result.Keys);
        Assert.Equal("Naam is verplicht", result[nameof(BoatEntity.Name)]);
    }

    [Fact]
    public void ValidateForCreate_ShouldReturnError_WhenBoatTypeIdIsInvalid()
    {
        // Arrange
        var boat = new BoatEntity
        {
            Name = "Valid Boat",
            BoatTypeId = 99
        };
        var validator = new BoatValidator(new MockBoatTypeRepository());

        // Act
        var result = validator.ValidateForCreate(boat);

        // Assert
        Assert.Contains(nameof(BoatEntity.BoatTypeId), result.Keys);
        Assert.Equal("Boottype is verplicht", result[nameof(BoatEntity.BoatTypeId)]);
    }

    [Fact]
    public void ValidateForCreate_ShouldReturnNoErrors_WhenBoatIsValid()
    {
        // Arrange
        var boat = new BoatEntity
        {
            Name = "Valid Boat",
            BoatTypeId = 1
        };
        var validator = new BoatValidator(new MockBoatTypeRepository());

        // Act
        var result = validator.ValidateForCreate(boat);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ValidateForUpdate_ShouldReturnNoErrors_WhenBoatIsValid()
    {
        // Arrange
        var validator = new BoatValidator(new MockBoatTypeRepository());
        BoatEntity boat = new BoatEntity
        {
            Name = "Valid Boat 2",
            BoatTypeId = 1,
            BoatId = 1,
            DeleteRequestDate = null,
            Status = BoatStatus.Operational
        };
        // Act
        var result = validator.ValidateForUpdate(boat);

        // Assert
        Assert.Empty(result);
    }

    [Theory]
    [InlineData(null, null, BoatStatus.Maintaining)]
    [InlineData("Boot des boten", "2024-12-02", BoatStatus.Operational)]
    public void ValidateForUpdate_ShouldReturnError_WhenBoatIsInvalid(string name, string requestDateString, BoatStatus status)
    {
        // Arrange

        var requestDate = string.IsNullOrEmpty(requestDateString) ? (DateTime?)null : DateTime.Parse(requestDateString);
        var validator = new BoatValidator(new MockBoatTypeRepository());
        BoatEntity boat = new BoatEntity
        {
            Name = name,
            BoatTypeId = 1,
            BoatId = 1,
            DeleteRequestDate = requestDate,
            Status = status
        };

        // Act
        var result = validator.ValidateForUpdate(boat);

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public void IsValidForPermanentDeletion_ShouldReturnError_WhenRequestDateIsNull()
    {
        // Arrange
        var boat = new BoatEntity
        {
            DeleteRequestDate = null
        };
        var validator = new BoatValidator(new MockBoatTypeRepository());

        // Act
        var result = validator.IsValidForPermanentDeletion(boat);

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public void IsValidForPermanentDeletion_ShouldReturnError_WhenRequestDateIsNotDue()
    {
        // Arrange
        var boat = new BoatEntity
        {
            DeleteRequestDate = DateTime.Now
        };
        var validator = new BoatValidator(new MockBoatTypeRepository());

        // Act
        var result = validator.IsValidForPermanentDeletion(boat);

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public void IsValidForPermanentDeletion_ShouldReturnNoErrors_WhenWaitTimeExpired()
    {
        // Arrange
        BoatValidator.RequestDeletionTime = TimeSpan.FromSeconds(1);
        var boat = new BoatEntity
        {
            DeleteRequestDate = DateTime.Now
        };
        var validator = new BoatValidator(new MockBoatTypeRepository());

        // Act
        Thread.Sleep(1001);
        var result = validator.IsValidForPermanentDeletion(boat);

        // Assert
        Assert.Empty(result);

        // Reset
        BoatValidator.RequestDeletionTime = TimeSpan.FromMinutes(30);
    }
}