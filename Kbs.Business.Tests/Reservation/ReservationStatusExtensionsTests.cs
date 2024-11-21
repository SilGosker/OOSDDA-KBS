namespace Kbs.Business.Reservation;

public class ReservationStatusExtensionsTests
{
    [Theory]
    [InlineData(ReservationStatus.Active, "Actief")]
    [InlineData(ReservationStatus.Deleted, "Verwijderd")]
    [InlineData(ReservationStatus.Expired, "Afgerond")]
    [InlineData((ReservationStatus)0, "Onbekend")]
    [InlineData((ReservationStatus)int.MaxValue, "Onbekend")]
    public void ToDutchString_ShouldReturnString(ReservationStatus status, string expected)
    {
        // Act
        var result = status.ToDutchString();

        // Assert
        Assert.Equal(expected, result);
    }
}