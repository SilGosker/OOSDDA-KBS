namespace Kbs.Business.BoatType;

public class BoatTypeSeatsExtensionsTests
{
    [Theory]
    [InlineData(BoatTypeSeats.One, "Één zitplaats")]
    [InlineData(BoatTypeSeats.Two, "Twee zitplaatsen")]
    [InlineData(BoatTypeSeats.Three, "Drie zitplaatsen")]
    [InlineData(BoatTypeSeats.Four, "Vier zitplaatsen")]
    [InlineData(BoatTypeSeats.Eight, "Acht zitplaatsen")]
    [InlineData((BoatTypeSeats)int.MaxValue, "Onbekend")]
    [InlineData((BoatTypeSeats)0, "Onbekend")]
    public void ToDutchString_ReturnsDutchString(BoatTypeSeats seats, string expected)
    {
        // Act
        var result = seats.ToDutchString();

        // Assert
        Assert.Equal(expected, result);
    }
}