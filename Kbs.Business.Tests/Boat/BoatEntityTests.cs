namespace Kbs.Business.Boat;

public class BoatEntityTests
{
    [Theory]
    [InlineData(1, "Boat 1", 1, BoatStatus.Broken)]
    [InlineData(-1, null, int.MinValue, BoatStatus.Maintaining)]
    [InlineData(int.MaxValue, "", -1, (BoatStatus)0)]
    public void Properties_ShouldSetValues(int id, string name, int boatTypeId, BoatStatus status)
    {
        // Arrange
        var boat = new BoatEntity();

        // Act
        boat.BoatID = id;
        boat.Name = name;
        boat.BoatTypeId = boatTypeId;
        boat.Status = status;

        // Assert
        Assert.Equal(id, boat.BoatID);
        Assert.Equal(name, boat.Name);
        Assert.Equal(boatTypeId, boat.BoatTypeId);
        Assert.Equal(status, boat.Status);
    }
}