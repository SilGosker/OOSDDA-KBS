namespace Kbs.Business.Boat;

public class BoatStatusExtensionsTests
{
    [Theory]
    [InlineData(BoatStatus.Broken, "Kapot")]
    [InlineData(BoatStatus.Maintaining, "In onderhoud")]
    [InlineData(BoatStatus.Operational, "Operationeel")]
    [InlineData((BoatStatus)int.MaxValue, "Onbekend")]
    [InlineData((BoatStatus)0, "Onbekend")]
    public void ToDutchString_ShouldReturnString(BoatStatus status, string expected)
    {
        // Act
        var actual = status.ToDutchString();

        // Assert
        Assert.Equal(expected, actual);
    }
}