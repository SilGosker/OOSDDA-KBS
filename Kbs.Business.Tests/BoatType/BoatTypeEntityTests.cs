namespace Kbs.Business.BoatType;

public class BoatTypeEntityTests
{

    [Theory]
    [InlineData(1, "Kano 1", BoatTypeRequiredExperience.Beginner, 4, 20, true)]
    [InlineData(int.MaxValue, null, (BoatTypeRequiredExperience)0, -20, 800, false)]
    [InlineData(int.MinValue, "", (BoatTypeRequiredExperience)100, int.MaxValue, -1, true)]
    public void Properties_ShouldSetValues(int id, string name, BoatTypeRequiredExperience experience, int seats, int speed, bool hasSteeringWheel)
    {
        // Arrange
        var boatType = new BoatTypeEntity();

        // Act
        boatType.BoatTypeID = id;
        boatType.Name = name;
        boatType.RequiredExperience = experience;
        boatType.Seats = seats;
        boatType.Speed = speed;
        boatType.HasSteeringWheel = hasSteeringWheel;

        // Assert
        Assert.Equal(boatType.BoatTypeID, id);
        Assert.Equal(boatType.Name, name);
        Assert.Equal(boatType.RequiredExperience, experience);
        Assert.Equal(boatType.Seats, seats);
        Assert.Equal(boatType.Speed, speed);
        Assert.Equal(boatType.HasSteeringWheel, hasSteeringWheel);
    }
}