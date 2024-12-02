namespace Kbs.Business.Damage;

public class DamageEntityTests
{
    [Theory]
    [InlineData(1, 1, DamageStatus.Solved, "Test Description", "2022-01-01", new byte[] { 0x20, 0x21, 0x22 })]
    [InlineData(0, 0, DamageStatus.UnSolved, "", "", new byte[] { })]
    [InlineData(int.MinValue, int.MinValue, (DamageStatus)0, null, null, null)]
    public void Properties_ShouldSetValues(int id, int boatId, DamageStatus status, string description, DateTime dateReported, byte[] image)
    {
        // Arrange
        var damage = new DamageEntity();
        
        // Act
        damage.DamageId = id;
        damage.BoatId = boatId;
        damage.Status = status;
        damage.DamageDescription = description;
        damage.DateReported = dateReported;
        damage.Image = image;
        
        // Assert
        Assert.Equal(id, damage.DamageId);
        Assert.Equal(boatId, damage.BoatId);
        Assert.Equal(status, damage.Status);
        Assert.Equal(description, damage.DamageDescription);
        Assert.Equal(dateReported, damage.DateReported);
        Assert.Equal(image, damage.Image);
    }
}