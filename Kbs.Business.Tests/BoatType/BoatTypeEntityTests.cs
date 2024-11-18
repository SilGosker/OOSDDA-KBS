namespace Kbs.Business.BoatType;

public class BoatTypeEntityTests
{

    [Theory]
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