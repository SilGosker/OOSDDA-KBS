namespace Kbs.Business.Damage;

public class DamageStatusExtensionsTests
{
    [Theory]
    [InlineData(DamageStatus.UnSolved, "Onopgelost")]
    [InlineData(DamageStatus.Solved, "Opgelost")]
    [InlineData((DamageStatus)0, "Onbekend")]
    [InlineData((DamageStatus)int.MaxValue, "Onbekend")]
    public void ToDutchString_ShouldReturnDutchString(DamageStatus status, string expected)
    {
        // Arrange & Act
        var actual = status.ToDutchString();

        // Assert
        Assert.Equal(expected, actual);
    }
}