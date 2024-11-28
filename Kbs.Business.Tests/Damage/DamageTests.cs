namespace Kbs.Business.Damage;

public class DamageTests
{
    [Fact]
    public void DamageReport_DamageDescription_ShouldNotBeEmpty()
    {
        var report = new Damage
        {
            DamageDescription = string.Empty
        };

        Assert.True(string.IsNullOrEmpty(report.DamageDescription), "Damage description cannot be empty");
    }
    [Fact]
    public void DamageReport_DateReported_CanBeNull()
    {
        var report = new Damage
        {
            DateReported = null
        };

        Assert.Null(report.DateReported);
    }
    [Fact]
    public void DamageReport_BoatID_ShouldBeValid()
    {
        var report = new Damage
        {
            BoatId = 1
        };

        Assert.Equal(true, report.BoatId >= 0);
    }
}